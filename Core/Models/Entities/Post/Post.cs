using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities.Post
{
    public class Post : BaseId
    {
        public string Body1 { get; set; }
        public string Body2 { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime DeleteDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public int LikesCount { get; set; }
        public int FavesCount { get; set; }
        public int DislikesCount { get; set; }
        public int WowsCount { get; set; }
        public int LaughsCount { get; set; }
        public int SadsCount { get; set; }
        public int AngersCount { get; set; }
        public int CommentsCount { get; set; }
        public int ViewsIpCount { get; set; }
        public int ViewsUsersCount { get; set; }
        public int ViewsTotalCount { get; set; }
    }
}
