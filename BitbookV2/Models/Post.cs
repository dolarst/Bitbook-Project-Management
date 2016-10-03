using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class Post
    {
        public int Id { set; get; }

        public string PostText { set; get; }

        

        public virtual Registration PostOwnedUser { set; get; }

        public virtual Registration PostSharedUser { set; get; }

        public int PostOwnedUserId { set; get; }

        public int PostSharedUserId { set; get; }

        public List<Comment> Comments { set; get; }

        public List<Like> Likes { set; get; }

        public DateTime? DateTime { set; get; }

        [NotMapped]
        public Image Image { get; set; }

        [NotMapped]
        public List<Registration> LikedUserList { set; get; } 
        
    }
}