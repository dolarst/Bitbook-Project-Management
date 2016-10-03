using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeDateOfBirth
    {

        [Required(ErrorMessage = "Please enter your date of birth.")]
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { set; get; }


    }
}