using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class FriendRelation
    {
       

        public int UserId { set; get; }

        public virtual Registration User { set; get; }

        public int FriendId { set; get; }

        public  virtual Registration Friend { set; get; }

        public int Created { set; get; }

        
        public DateTime? DateTime { set; get; }
    }
}