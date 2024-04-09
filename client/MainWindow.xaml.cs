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
                        ShowAddPost();
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

        private void ShowHome()
        {
            contentFrame.Content = new HomePage();
        }

        private void ShowAddPost()
        {
            contentFrame.Content = new AddPostPage();
        }

        private void ShowSavedPosts()
        {
            contentFrame.Content = new SavedPostsPage();
        }

        private void ShowArchivedPosts()
        {
            contentFrame.Content = new ArchivedPostsPage();
        }
    }
}
