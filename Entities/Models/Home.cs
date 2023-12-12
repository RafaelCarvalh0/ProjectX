using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class HomePageResponse
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FeedText { get; set; }
        public int LikeQuantity {  get; set; }
        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; }
        public int CommentQuantity { get; set; }
    }
}
