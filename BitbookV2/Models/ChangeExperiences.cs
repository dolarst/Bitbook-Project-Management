using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeExperiences
    {
        [Required(ErrorMessage = "Please enter experience details.")]
        public string Experiences { set; get; }
    }
}