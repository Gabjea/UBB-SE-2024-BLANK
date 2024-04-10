using client.models;
using client.repositories;
using client.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace client
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private MainService service;
        public ObservableCollection<Post> Posts { get; set; }

        private Frame contentFrame;

		internal HomePage(MainService mainService, Frame _contentFrame)
        {
            InitializeComponent();
            service = mainService;
            contentFrame = _contentFrame;
            // Fetch posts from the repository
            Posts = new ObservableCollection<Post>(service.getAllPosts());
         
            PostsControl.Items.Clear();
            DataContext = this;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(service);
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.Content = loginPage;
            }
        }

        private void ReportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ReportModalWindow reportModal = new ReportModalWindow();
            reportModal.Owner = Window.GetWindow(this);
            reportModal.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void shareButton1_Click(object sender, RoutedEventArgs e)
        {
            SharePostWindow sharePost = new SharePostWindow();
            sharePost.Owner = Window.GetWindow(this);
            sharePost.ShowDialog();
        }

        private void openPostButton1_Click(object sender, RoutedEventArgs e)
        {
            PostWindow post = new PostWindow();
            post.Owner = Window.GetWindow(this);
            post.ShowDialog();
        }

        private void openPostButton2_Click(object sender, RoutedEventArgs e)
        {

        }

		private void LikesCount_Click(object sender, RoutedEventArgs e)
		{
			// Get the Button that triggered the event
			Button button = sender as Button;

			// Get the DataContext of the Button, which should be the Post object
			if (button?.DataContext is Post post)
			{
				// Access the Post object's properties, such as its Id
				Guid postId = post.id;
                service.PostsService.addReactionToPost(postId, post.ownerUserID, 0);
				// Now you can use the postId as needed
				contentFrame.Content = new HomePage(service,contentFrame);
			}
		}


	}
}
