using client.models;
using client.repositories;
using client.services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application

	{
		App()
		{
			MainWindow m = new MainWindow();
			m.Show();

			PostsService postsService = new PostsService();
			PostsRepository postsRepository = new PostsRepository(postsService);
			List<Guid> users = new List<Guid>();
			Post post = new Post(Guid.NewGuid(), "mata", Guid.Parse("d5711b72-7e1f-4a8d-9226-38f6da717a77"), users, Guid.NewGuid(), Guid.NewGuid(), new Media(""), 0,"erthe", DateTime.Now);
			//postsRepository.addPostToDB(post);
			//postsRepository.removePostFromDB(Guid.Parse("2035757E-881B-4035-B2F1-A5DD0A6C8E51"));

			postsRepository.updatePostDescription(Guid.Parse("45A83770-98AA-4631-88BF-E782CDFA3A00"), "il fut pe fedus");
			postsRepository.updatePostLocation(Guid.Parse("BDF1733F-615C-40CA-9D40-2E65369B911F"), "il fut pe florin");

			//postsRepository.addReactionToPost(Guid.Parse("BDF1733F-615C-40CA-9D40-2E65369B911F"), Guid.Parse("d5711b72-7e1f-4a8d-9226-38f6da717a77"), 0);
			//postsRepository.removeReactionToPost(Guid.Parse("BDF1733F-615C-40CA-9D40-2E65369B911F"), Guid.Parse("d5711b72-7e1f-4a8d-9226-38f6da717a77"));
			//List<Post> posts = postsRepository.getAllPosts();
			//posts.ForEach(post => MessageBox.Show(post.ToString()));

			//List<Post> posts = postsRepository.getAllPostsFromLocation("fedus");
			//posts.ForEach(post => MessageBox.Show(post.ToString()));

		}
	}

}
