using client.models;
using client.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services
{
	internal class PostsService
	{
		private PostsRepository repository;
		public PostsService(PostsRepository _repository) {
			repository = _repository;

		}

		public bool addPost(Guid owner_user_id,String? description,List<Guid> mentionedUsers,Guid? commented_post_id,Guid? original_post_id,Media? media, int post_type,String? location_id)
		{
			Post newPost = new Post(Guid.NewGuid(),description,owner_user_id,mentionedUsers,commented_post_id,original_post_id,media,post_type,location_id,DateTime.Now);

			if(repository.addPostToDB(newPost))
			{
				return true;
			}
			return false;
		}

		public bool removePost(Guid post_id)
		{
			if (repository.removePostFromDB(post_id))
			{
				return true;
			}

			return false;
		}

		public bool updateDescription(Guid post_id,String newDescription)
		{
			if (repository.updatePostDescription(post_id, newDescription))
			{
				return true;
			}
			return false;
		}

		public bool updateLocation(Guid post_id,String newLocationID)
		{
			if (repository.updatePostLocation(post_id, newLocationID))
			{
				return true;
			}
			return false;
		}
		public bool addReactionToPost(Guid postID, Guid userID, int reactionType)
		{
			if (repository.addReactionToPost(postID, userID, reactionType))
			{
				return true;
			}
			return false;
		}

		public bool removeReactionToPost(Guid postID, Guid userID)
		{
			if (repository.removeReactionToPost(postID, userID))
			{
				return true;
			}
			return false;
		}	
		public List<Post> getAllPosts() { 
			return repository.getAllPosts();
		}

		public List<Post> getAllPostsFromLocation(String location_id) {
			return repository.getAllPostsFromLocation(location_id);
		}
}
}
