using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class LoginOrRegistrationViewModel
    {
        public Models.Login Login { set; get; }

        public Models.Registration Registration { set; get; }
    }
}