using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangePhoneNo
    {
        [Required(ErrorMessage = "Please enter your phone number")]
        [DisplayName("Phone Number")]
        public string PhoneNo { set; get; }
    }
}