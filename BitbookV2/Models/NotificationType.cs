using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class NotificationType
    {
        public int Like { set; get; }

        public int Comment { set; get; }
        public int PostToWall { set; get; }
        public int CommentOnSharedPost { set; get; }
        public int ChangeProfilePicture { set; get; }
        public int ChangeCoverPicture { set; get; }
        public int LikeOnSharedPost { set; get; }

        public NotificationType()
        {
            Like = 0;
            Comment = 1;
            PostToWall = 2;
            ChangeProfilePicture = 3;
            ChangeCoverPicture = 4;
            CommentOnSharedPost = 5;
            LikeOnSharedPost = 6;
        }
    }
}