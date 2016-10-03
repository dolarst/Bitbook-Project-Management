using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeAreaOfInterest
    {
        [Required(ErrorMessage = "Please enter your area of interests.")]
        [DisplayName("Area of Interest")]
        public string AreaOfInterest { set; get; }
    }
}