using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace client
{
	public partial class AddPostPage : Page
	{
		private HashSet<string> selectedUsersSet = new HashSet<string>();
		private String description = "";
		private String filepath = "";
		private string selectedLocation = "";


		// Simulated list of users (replace with actual user data)
		private List<string> allUsersList = new List<string>()
		{
			"Friend 1",
			"Friend 2",
			"User 3",
			"User 4",
			"User 5"
		};

		public AddPostPage()
		{
			InitializeComponent();
			PopulateUserSearchList(allUsersList);
		}

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			// Step 1: Adding a text description - Hide this panel and show the next panel
			txtDescriptionPanel.Visibility = Visibility.Collapsed; // Hide text description panel
			mediaUploadPanel.Visibility = Visibility.Visible; // Show media upload panel
			description = txtDescription.Text;
		}

		private void UploadMediaButton_Click(object sender, RoutedEventArgs e)
		{
			UploadMedia();
		}

		private void UploadMedia()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif|All files (*.*)|*.*";
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); // Set initial directory

			if (openFileDialog.ShowDialog() == true)
			{
				// The user selected a file
				string filePath = openFileDialog.FileName;
				filepath = filePath;

				// Set the source of the image preview control
				imagePreview.Source = new BitmapImage(new Uri(filePath));
				imagePreview.Visibility = Visibility.Visible; // Show the image preview

			}
		}

		private void NextButton2_Click(object sender, RoutedEventArgs e)
		{
			// Step 2: Uploading media - Hide this panel and show the next panel
			mediaUploadPanel.Visibility = Visibility.Collapsed; // Hide media upload panel
			locationPanel.Visibility = Visibility.Visible; // Show location selection panel
		}

		private void LocationSearchTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			// Perform location search based on user input and populate dropdown list
			string searchQuery = txtLocationSearch.Text.Trim();
			if (!string.IsNullOrWhiteSpace(searchQuery))
			{
				// Simulated location search results for demonstration
				List<string> locations = new List<string>()
		{
			"Location 1",
			"Location 2",
			"Location 3",
			"Location 4",
			"Location 5"
		};

				locationDropdown.ItemsSource = locations;
				locationDropdown.Visibility = Visibility.Visible; // Show dropdown list
			}
			else
			{
				locationDropdown.Visibility = Visibility.Collapsed; // Hide dropdown list if search query is empty
			}
		}
		private void LocationDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Update the selected location when the user selects an item from the dropdown list
			selectedLocation = locationDropdown.SelectedItem?.ToString();
		}

		private void NextButton3_Click(object sender, RoutedEventArgs e)
		{
			// Step 3: Selecting physical location - Hide this panel and show the next panel
			locationPanel.Visibility = Visibility.Collapsed; // Hide location selection panel
			mentionPanel.Visibility = Visibility.Visible; // Show mention users panel

			// Perform any other necessary actions when moving to the next step
		}

		private void UserSearchTextBox_KeyUp(object sender, RoutedEventArgs e)
		{
			string searchQuery = txtUserSearch.Text.Trim();

			// Filter the user list based on the search query
			List<string> filteredUsers = allUsersList.Where(user => user.ToLower().Contains(searchQuery.ToLower())).ToList();
			PopulateUserSearchList(filteredUsers);
		}

		// Method to update the ListBox with matching users
		private void PopulateUserSearchList(List<string> users)
		{
			// Clear the existing items from the ListBox
			userSearchListBox.Items.Clear();

			// Update the ListBox with matching users
			foreach (string user in users)
			{
				userSearchListBox.Items.Add(user);
			}
		}

		private void NextButton4_Click(object sender, RoutedEventArgs e)
		{
			// Step 4: Mentioning users - Hide this panel and show the next panel
			mentionPanel.Visibility = Visibility.Collapsed; // Hide mention users panel
			postPanel.Visibility = Visibility.Visible; // Show post panel

			// Store the selected users globally
			selectedUsersSet.Clear();
			foreach (string selectedUser in selectedUsersListBox.Items)
			{
				selectedUsersSet.Add(selectedUser);
			}
		}

		private void UserSearchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Move selected user from user search ListBox to selected users ListBox
			MoveSelectedUser(userSearchListBox, selectedUsersListBox);
		}

		private void SelectedUsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Move selected user from selected users ListBox to user search ListBox
			MoveSelectedUser(selectedUsersListBox, userSearchListBox);
		}

		private void MoveSelectedUser(ListBox sourceListBox, ListBox destinationListBox)
		{
			if (sourceListBox.SelectedItem != null)
			{
				string selectedUser = sourceListBox.SelectedItem.ToString();

				// Check if the user is already selected
				if (destinationListBox.Items.Contains(selectedUser))
				{
					MessageBox.Show("User already selected.");
					return;
				}

				// Remove from source ListBox
				sourceListBox.Items.Remove(selectedUser);

				// Add to destination ListBox
				destinationListBox.Items.Add(selectedUser);

				// Clear selection
				sourceListBox.SelectedItem = null;

				// Update selected users set
				if (destinationListBox == selectedUsersListBox)
				{
					selectedUsersSet.Add(selectedUser);
				}
				else
				{
					selectedUsersSet.Remove(selectedUser);
				}
			}
		}


		private void PostButton_Click(object sender, RoutedEventArgs e)
			{
			string mentions = "";
				foreach(string user in selectedUsersSet)
				{
				mentions += user + "\n"; 
				}

			MessageBox.Show(mentions);
			MessageBox.Show(description);
			MessageBox.Show(filepath);
			MessageBox.Show(selectedLocation);
			}
	}
}
