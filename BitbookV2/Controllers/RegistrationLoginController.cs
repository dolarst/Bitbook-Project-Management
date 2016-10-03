using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BitbookV2.Bitbook.core.BLL;
using BitbookV2.Context;
using BitbookV2.Models;
using ModelState = System.Web.ModelBinding.ModelState;
using System.Web.Security;

namespace BitbookV2.Controllers
{
    public class RegistrationLoginController : Controller
    {
     
        RegistrationManager registrationManager = new RegistrationManager();
        LoginManager loginManager = new LoginManager();
        // GET: RegistrationLogin
        public ActionResult RegistrationLogin()
        {
            return View();
        }

        [AllowAnonymous]    
        public JsonResult DoesEmailAddressExist([Bind(Prefix = "Registration.Email")]string email)
        {
            bool exists = registrationManager.DoesEmailAlreadyExist(email);
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Register(Models.LoginOrRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Registration registration = model.Registration;

                var user  = registrationManager.RegisterUser(registration);

                if (user != null)
                {


                    Session["Registration"] = user;
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("ProfilePage", "Profile", new {id = user.Id});
                }
            }

            ModelState.AddModelError("", "Something went wrong.Make sure all the fields have been entered and that the password entered is atleast 8 characters long. Also make sure that the passwords match in both the fields and that no other account has been previously made with the email address you entered.");

            return View("RegistrationLogin");
        }

        [HttpPost]
        public ActionResult Login(Models.LoginOrRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.Login login = model.Login;

                Registration registration = loginManager.LoginValidation(login);

                if (registration != null)
                {
                    Session["Registration"] = registration;
                    FormsAuthentication.SetAuthCookie(registration.Email,false);
                    return RedirectToAction("NewsfeedHome","Newsfeed");

                }

                else
                {
                    ModelState.AddModelError("","Email and Password does not match.");
                }

            }
            
            return View("RegistrationLogin");
        }

        [AllowAnonymous]
        public JsonResult IsEmailValid([Bind(Prefix = "Login.Email")]string email)
        {
            bool isValid = registrationManager.DoesEmailAlreadyExist(email);
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        
    }
}