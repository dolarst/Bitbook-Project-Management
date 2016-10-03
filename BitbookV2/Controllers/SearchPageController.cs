using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitbookV2.Bitbook.core.BLL;
using BitbookV2.Custom.Attributes;
using BitbookV2.Models;

namespace BitbookV2.Controllers
{

    [NoCache]
    [Authorize]
    public class SearchPageController : Controller
    {
        RegistrationManager registrationManager = new RegistrationManager();

        // GET: SearchPage
        public ActionResult SearchPageView(string text)
        {
            SearchPageViewModel searchPageViewModel = new SearchPageViewModel();

            searchPageViewModel.Users = registrationManager.GetUserListByPartialText(text);

            return View(searchPageViewModel);
        }


        [HttpPost]
        public ActionResult SearchPageViewInPage(string text)
        {
            SearchPageViewModel searchPageViewModel = new SearchPageViewModel();

            searchPageViewModel.Users = registrationManager.GetUserListByPartialText(text);

            return View("SearchPageView",searchPageViewModel);
        }

        public ActionResult GetProfilePictureByProfileId(int id)
        {
            var image = registrationManager.GetProfilePictureByProfileId(id);
            var imageData = image.ImageData;

            if (imageData != null)
            {
                return File(imageData, "image/jpg");
            }
            else
            {
                return null;
            }

        }


    }
}