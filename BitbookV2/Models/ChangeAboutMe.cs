using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeAboutMe
    {
        [Required(ErrorMessage = "Please write something in About section.")]
        [DisplayName("About")]
        public string AboutMe { set; get; }

    }
}