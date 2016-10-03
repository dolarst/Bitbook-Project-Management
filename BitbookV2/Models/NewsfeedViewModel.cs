using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class NewsfeedViewModel
    {
        public List<Post> Posts { set; get; }
       
        public Post Post;

        public Registration LoggedInUser { set; get; }
    }
}