using client.models;
using client.services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace client.repositories
{
	internal class PostsRepository
	{	
		private PostsService service;

		private DatabaseConnection dbInstance;
		private SqlConnection conn;
		public PostsRepository(PostsService _service) {
			dbInstance = DatabaseConnection.Instance;
			conn = dbInstance.GetConnection();
			service = _service;
		}

		public bool addPostToDB(Post post)
		{
			
				string query = "INSERT INTO posts (post_id, owner_user_id, description, commented_post_id, original_post_id, media_path, post_type, location_id, created_date) VALUES (@post_id, @owner_user_id, @description, @commented_post_id, @original_post_id , @media_path, @post_type, @location_id, @created_date)";

				conn.Open();
				using (SqlCommand command = new SqlCommand(query, conn))
				{
					// Add parameters to the command to prevent SQL injection
					command.Parameters.AddWithValue("@post_id", post.id);
					command.Parameters.AddWithValue("@owner_user_id", post.ownerUserID);
					command.Parameters.AddWithValue("@description", post.description);
					command.Parameters.AddWithValue("@commented_post_id", post.commentedPostID);
					command.Parameters.AddWithValue("@original_post_id", post.originalPostID);
					command.Parameters.AddWithValue("@media_path", post.media?.FilePath);
					command.Parameters.AddWithValue("@post_type", post.postType);
					command.Parameters.AddWithValue("@location_id", post.locationID);
					command.Parameters.AddWithValue("@created_date", post.createdDate);

					try
					{
						
						int rowsAffected = command.ExecuteNonQuery();
						
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error: " + ex.Message);
					conn.Close();
					return false;
					}
				}
				conn.Close();
				return true; 
		
		}

		public bool removePostFromDB(Guid postID)
		{
			string query = "DELETE FROM posts WHERE post_id = @post_id";

			conn.Open();
			using (SqlCommand command = new SqlCommand(query, conn))
			{
				command.Parameters.AddWithValue("@post_id", postID);
				
				try
				{
					int rowsAffected = command.ExecuteNonQuery();
					
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message);
					conn.Close();
					return false;
				}
			}
			conn.Close();
			return true;
		}

		public bool addReactionToPost(Guid postID, Guid userID, int reactionType)
		{
			string query = "INSERT INTO reactions (post_id, user_id, reaction) VALUES (@post_id, @user_id, @reaction)";

			conn.Open();
			using (SqlCommand command = new SqlCommand(query, conn))
			{
				// Add parameters to the command to prevent SQL injection
				command.Parameters.AddWithValue("@post_id", postID);
				command.Parameters.AddWithValue("@user_id", userID);
				command.Parameters.AddWithValue("@reaction", reactionType);
				

				try
				{

					int rowsAffected = command.ExecuteNonQuery();

				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message);
					conn.Close();
					return false;
				}
			}
			conn.Close();
			return true;
		}
		public bool removeReactionToPost(Guid postID, Guid userID)
		{
			string query = "DELETE FROM reactions WHERE post_id = @post_id AND user_id = @user_id";

			conn.Open();
			using (SqlCommand command = new SqlCommand(query, conn))
			{
				command.Parameters.AddWithValue("@post_id", postID);
				command.Parameters.AddWithValue("@post_id", userID);

				try
				{
					int rowsAffected = command.ExecuteNonQuery();

				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.Message);
					conn.Close();
					return false;
				}
			}
			conn.Close();
			return true;
		}

        public bool updatePostDescription(Guid postID, string newDescription)
		{
		
				string query = "UPDATE posts SET description = @description WHERE post_id = @post_id";
				conn.Open();

				using (SqlCommand command = new SqlCommand(query, conn))
				{
					// Add parameters for the values to be updated and the record ID
					command.Parameters.AddWithValue("@post_id", postID);
					command.Parameters.AddWithValue("@description", newDescription);

					try
					{
						int rowsAffected = command.ExecuteNonQuery();
						
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: " + ex.Message);
						conn.Close();
					return false;
					}
				}
				conn.Close();
			return true;
		}
		public bool updatePostLocation(Guid postID, String newLocationID)
		{
			string query = "UPDATE posts SET location_id = @location_id WHERE post_id = @post_id";
			conn.Open();

			using (SqlCommand command = new SqlCommand(query, conn))
			{
				// Add parameters for the values to be updated and the record ID
				command.Parameters.AddWithValue("@post_id", postID);
				command.Parameters.AddWithValue("@location_id", newLocationID);

				try
				{
					int rowsAffected = command.ExecuteNonQuery();

				}
				catch (Exception ex)
				{
					Console.WriteLine("Error: " + ex.Message);
					conn.Close();
					return false;
				}
			}
			conn.Close();
			return true;
		}
		public List<Post> getAllPosts() {
			string query = "SELECT * FROM posts";

			List<Post> posts = new List<Post>();
			using (SqlCommand command = new SqlCommand(query, conn))
			{
				
			
				conn.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						
						Guid post_id = Guid.Parse(reader.GetString(0));
						Guid owner_user_id = Guid.Parse(reader.GetString(1));
						String? description = reader.GetString(2);
						Guid? commented_post_id = Guid.Parse(reader.GetString(3));
						Guid? original_post_id = Guid.Parse(reader.GetString(4));
						String? media_path = reader.GetString(5);
						int post_type = reader.GetInt16(6);
						String? location_id  = reader.GetString(7);
						DateTime created_date = reader.GetDateTime(8);
						Post post = new Post(post_id,description,owner_user_id,new List<Guid>(),commented_post_id,original_post_id,new Media(media_path,".jpg"),post_type,location_id,created_date);
						
						posts.Add(post);
					}
				}
			}
				conn.Close();
				return posts;

		}
		public List<Post> getAllPostsFromLocation(String locationID) {
			string query = "SELECT * FROM posts WHERE location_id = @location_id";
			List<Post> posts = new List<Post>();

			conn.Open();
			using (SqlCommand command = new SqlCommand(query, conn))
			{

				command.Parameters.AddWithValue("@location_id", locationID);

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{

						Guid post_id = Guid.Parse(reader.GetString(0));
						Guid owner_user_id = Guid.Parse(reader.GetString(1));
						String? description = reader.GetString(2);
						Guid? commented_post_id = Guid.Parse(reader.GetString(3));
						Guid? original_post_id = Guid.Parse(reader.GetString(4));
						String? media_path = reader.GetString(5);
						int post_type = reader.GetInt16(6);
						String? location_id = reader.GetString(7);
						DateTime created_date = reader.GetDateTime(8);
						Post post = new Post(post_id, description, owner_user_id, new List<Guid>(), commented_post_id, original_post_id, new Media(media_path, ".jpg"), post_type, location_id, created_date);

						posts.Add(post);
					}
				}
			}
			conn.Close();
			return posts;
		}
	}
}
