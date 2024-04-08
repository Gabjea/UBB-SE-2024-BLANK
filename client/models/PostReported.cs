using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class PostReported
    {
        public Guid ReportedPostId { get; set; }
        public Guid PostId { get; set; }
        public string message { get; set; }
        public Guid ReporterId { get; set; }

        public PostReported(Guid reportedPostId, Guid postId, string message, Guid reporterId)
        {
            this.ReportedPostId = reportedPostId;
            this.PostId = postId;
            this.message = message;
            this.ReporterId = reporterId;
        }

        

        public override string ToString()
        {
            return "PostReported{" +
                    "ReportedPostId=" + ReportedPostId +
                    ", PostId=" + PostId +
                    ", message='" + message + '\'' +
                    ", ReporterId=" + ReporterId +
                    '}';
        }
    }
}
