using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BitbookV2.Models
{
    public class Registration
    {
 
   
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide your first name.")]
        [DisplayName("First Name")]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "Please provide your last name.")]
        [DisplayName("Last Name")]
        public string LastName { set; get; }

        [Required(ErrorMessage = "Please provide your gender.")]
        public string Gender { set; get; }

        [Required(ErrorMessage = "Please provide your email address.")]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please provide valid email address.")]
       [Remote("DoesEmailAddressExist", "RegistrationLogin",ErrorMessage = "Email Address already exists.")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Please provide your password.")]
        [MinLength(8,ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { set; get; }

        [Required(ErrorMessage = "Please provide your birth date.")]
        public DateTime BirthDate { set; get; }

        [Required(ErrorMessage = "Please provide your password again.")]
        [DisplayName("Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
 

        public string PasswordConfirm { set; get; }

        public List<Post> PostOwned { set; get; }

        public List<Post> PostShared { set; get; } 


      
     
    }
}