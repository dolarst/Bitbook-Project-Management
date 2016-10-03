using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeGender
    {
        [Required(ErrorMessage = "Please select you gender.")]
        public string Gender { set; get; }


    }
}