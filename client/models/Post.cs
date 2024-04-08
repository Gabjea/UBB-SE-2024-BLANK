using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace client.models
{   
    class Media { }
    class Post
    {

        private Guid id { get; set; }
        private String description { get; set; }
        private Guid ownerUserID {  get; set; }
        private List<Guid> mentionedUsers { get; set; }
        private Guid commentedPostID {  get; set; }
        private Guid originalPostID { get; set; }
        private Media media { get; set; }
        private int postType { get; set; }
        private String locationID { get; set; }
        private DateTime createdDate { get; set; }

        public Post(String _description, Guid _onwerUserID, List<Guid> _mentionedUsers, Guid _commentedPostID, Guid _orignalPostID, Media _media, int _postType, String _locationID, DateTime _createdDate)
		{
            id = Guid.NewGuid();
            description = _description;
            ownerUserID = _onwerUserID;
            mentionedUsers = _mentionedUsers;
            commentedPostID =   _commentedPostID;
            originalPostID = _orignalPostID;
            media = _media;
            postType = _postType;
            locationID = _locationID;
            createdDate = _createdDate;
		}
        public override string ToString()
        {
            return $"Post : {id}\n{description}\n{ownerUserID}\n{mentionedUsers}\n{commentedPostID}\n{originalPostID}\n{media}\n{postType}\n{locationID}\n{createdDate}";
        }
    }
}
