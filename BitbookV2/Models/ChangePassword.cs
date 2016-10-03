using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitbookV2.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please put in your current password.")]
        [Remote("IsPasswordValid", "Profile", ErrorMessage = "This is not the correct password.")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Please put in a new password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string NewPassword { set; get; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword")]
        public string ConfirmPassword { set; get; }
    }
}