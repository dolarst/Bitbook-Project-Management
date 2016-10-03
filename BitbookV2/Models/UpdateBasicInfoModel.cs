using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class UpdateBasicInfoModel
    {
        [Required(ErrorMessage = "Required")]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "Required")]
        public string LastName { set; get; }

        [Required(ErrorMessage = "Required")]
        public string Gender { set; get; }

        [Required(ErrorMessage = "Required")]
        public DateTime DateOfBirth { set; get; }

        [Required(ErrorMessage = "Required")]
        public string Experiences { set; get; }

        [Required(ErrorMessage = "Required")]
        public string AboutMe { set; get; }

        [Required(ErrorMessage = "Required")]
        public string AreaOfInterest { set; get; }

        [Required(ErrorMessage = "Required")]
        public string City { set; get; }

        [Required(ErrorMessage = "Required")]
        public string Country { set; get; }

        [Required(ErrorMessage = "Required")]
        public string ContactEmail { set; get; }

        [Required(ErrorMessage = "Required")]
        public string PhoneNo { set; get; }



    }
}