using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeCountry
    {
        [Required(ErrorMessage = "Please enter your country of residence.")]
        public string Country { set; get; }
    }
}