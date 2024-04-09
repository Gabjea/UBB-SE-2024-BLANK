using System;
using System.Collections.Generic;
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
        public HomePage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();

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
            if (postOptions1.SelectedItem != null && ((ComboBoxItem)postOptions1.SelectedItem).Name == "ReportMenuItem1")
            {
                ReportModalWindow reportModal = new ReportModalWindow();
                reportModal.ShowDialog();
                postOptions1.SelectedIndex = -1;
            }

            if (postOptions2.SelectedItem != null && ((ComboBoxItem)postOptions2.SelectedItem).Name == "ReportMenuItem2")
            {
                ReportModalWindow reportModal = new ReportModalWindow();
                reportModal.ShowDialog();
                postOptions2.SelectedIndex = -1;
            }
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
