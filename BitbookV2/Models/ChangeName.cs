using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ChangeName
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        [DisplayName("First Name")]
        public string FirstName { set; get; }
      
        [Required(ErrorMessage = "Please enter your last name.")]
        [DisplayName("Last Name")]
        public string LastName { set; get; }


    }
}