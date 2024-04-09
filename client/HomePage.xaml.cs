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

        internal HomePage(MainService mainService)
        {
            InitializeComponent();
            service = mainService;

            // Fetch posts from the repository
            Posts = new ObservableCollection<Post>(service.PostsService.getAllPosts());
            DataContext = this;
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
    }
}
