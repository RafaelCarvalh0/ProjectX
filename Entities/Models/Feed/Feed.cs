using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Feed
{
    public class FeedRequest
    {
        public int id {  get; set; }
    }

    public class FeedResponse
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FeedText { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CommentCount { get; set; }
    }
    public class FeedCommentsResponse
    {
        public int FeedId { get; set; }
        public string FeedComment { get; set; }
        public int? FeedLikes {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
