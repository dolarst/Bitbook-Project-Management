using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BitbookV2.Context;

namespace BitbookV2.Bitbook.core.DAL
{
    public class LoginGateway
    {
        private BitbookDbContext db;

        public Models.Registration LoginValidation(Models.Login login)
        {
            db=new BitbookDbContext();

            Models.Registration registration =
                db.Registrations.Where(a => a.Email == login.Email && a.Password == login.Password).FirstOrDefault();

            return registration;
        }

    }
}