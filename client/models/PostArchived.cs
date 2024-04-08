using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class PostArchived
    {
        public Guid ArchivedPostId { get; set; }
        public Guid PostId { get; set; }

        public PostArchived(Guid archivedPostId, Guid postId)
        {
            this.ArchivedPostId = archivedPostId;
            this.PostId = postId;
        }

       
        public override string ToString()
        {
            return "PostArchived{" +
                    "archivedPostId=" + ArchivedPostId +
                    ", postId=" + PostId +
                    '}';
        }
    }
}
