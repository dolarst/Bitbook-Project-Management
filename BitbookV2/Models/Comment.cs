using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class Comment
    {
        public int Id { set; get; }

        public string CommentText { set; get; }

        
        public int PostId { set; get; }
       

        public int RegistrationId { set; get; }
        public virtual Registration Registration { set; get; }

        public DateTime? DateTime { set; get; }
       
        
    }
}