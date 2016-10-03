using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitbookV2.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please provide your email address.")]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please provide valid email address.")]
        [Remote("IsEmailValid", "RegistrationLogin", ErrorMessage = "No user under the entered email address exists.")]
        public string Email { set; get; }
        


        [Required(ErrorMessage = "Please provide your password.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { set; get; }
    }
}