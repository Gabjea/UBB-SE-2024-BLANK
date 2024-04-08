using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class PostSaved
    {
        public Guid SavedPostId { get;set; }
        public Guid SaverId { get;set; }
        public Guid PostId { get;set; }

        public PostSaved(Guid savedPostId, Guid saveUserId, Guid postId)
        {
            this.SavedPostId = savedPostId;
            this.SaverId = saveUserId;
            this.PostId = postId;
        }

       
        public override string ToString()
        {
            return "PostSaved{" +
                    "savedPostId=" + SavedPostId +
                    ", saveUserId=" + SaverId +
                    ", postId=" + PostId +
                    '}';
        }
    }
}
