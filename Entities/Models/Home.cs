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
        public DateTime? PostData { get; set; }
        public string PostItem { get; set; }
    }
}
