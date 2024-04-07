using System.Windows;
using System.Windows.Controls;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Initialize with View 1
            ShowHome();
            NavigationListBox.SelectionChanged += NavigationListBox_SelectionChanged;
        }

        private void NavigationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationListBox.SelectedItem != null)
            {
                string selectedItem = ((ListBoxItem)NavigationListBox.SelectedItem).Content.ToString();
                switch (selectedItem)
                {
                    case "Home":
                        ShowHome();
                        break;
                    case "Notifications":
                        // ShowView2();
                        break;
                    case "Messages":
                        // ShowView3();
                        break;
                    case "Add Post":
                        // ShowView4();
                        break;
                    case "Saved Posts":
                        ShowSavedPosts();
                        break;
                    case "Archived Posts":
                        ShowArchivedPosts();
                        break;
                    default:
                        break;
                }
            }
        }

        // View 1
        private void ShowHome()
        {
            contentFrame.Content = new HomePage();
        }

        // View 2
        private void ShowSavedPosts()
        {
            // Replace LoginPage() with your desired page
            contentFrame.Content = new SavedPostsPage();
        }

        // View 3
        private void ShowArchivedPosts()
        {
            // Replace LoginPage() with your desired page
            contentFrame.Content = new ArchivedPostsPage();
        }
    }
}
