using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeEducation
    {
        [Required(ErrorMessage = "Please enter your education details.")]
        public string Education { set; get; }

    }
}