using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeCity
    {
        [Required(ErrorMessage = "Please enter your city of residence.")]
        public string City { set; get; }
    }
}