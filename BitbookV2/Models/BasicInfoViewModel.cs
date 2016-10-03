using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class BasicInfoViewModel
    {

      
        public int Id { set; get; }

        public string Name { set; get; }

        public string Gender { set; get; }

        public string About { set; get; }

        public string Experiences { set; get; }

        public string Education { set; get; }

        public string City { set; get; }

        public string AreaOfInterest { set; get; }

        public string Country { set; get; }

        public string ContactEmail { set; get; }

        public string PhoneNo { set; get; }

        public DateTime? DateofBirth { set; get; }


        public int UserId { set; get; }
        public Registration User { set; get; }

        public int? ProfilePicId { set; get; }
        //public virtual Image ProfilePic { set; get; }

        public int? CoverPicId { set; get; }
        //public virtual Image CoverPic { set; get; }

    

    }
}