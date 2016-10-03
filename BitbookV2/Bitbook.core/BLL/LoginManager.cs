using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitbookV2.Bitbook.core.DAL;
using BitbookV2.Models;

namespace BitbookV2.Bitbook.core.BLL
{
    public class LoginManager
    {
        LoginGateway loginGateway = new LoginGateway();

        public Registration LoginValidation(Models.Login login)
        {
            return loginGateway.LoginValidation(login);
        }
    }
}