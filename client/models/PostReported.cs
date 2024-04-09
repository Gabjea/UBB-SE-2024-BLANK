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
        public String message { get; set; }
        public String reason { get; set;}
        public Guid ReporterId { get; set; }

        public PostReported(Guid reportedPostId, string _reason , string message, Guid postId,  Guid reporterId)
        {
            this.ReportedPostId = reportedPostId;
            this.PostId = postId;
            this.message = message;
            this.ReporterId = reporterId;
            this.reason = _reason;
        }

        

        public override String ToString()
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
