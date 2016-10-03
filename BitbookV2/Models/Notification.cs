using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class Notification
    {
        [Key]
        public int Id { set; get; }

        
        public int SenderId { set; get; }
        public virtual Registration Sender { set; get; }


       
        public int ReceiverId { set; get; }
        public virtual Registration Receiver {set; get; }

       
        public int? PostId { set; get; }
        public Post Post { set; get; }

        public int NotificationType { set; get; }

        public DateTime? DateTime { set; get; }
    }
}