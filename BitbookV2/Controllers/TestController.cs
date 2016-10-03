using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitbookV2.Bitbook.core.BLL;
using BitbookV2.Bitbook.core.DAL;
using BitbookV2.Models;

namespace BitbookV2.Controllers
{
    public class TestController : Controller
    {
        TestManager testManager = new TestManager();
        RegistrationManager registrationManager = new RegistrationManager();
        // GET: Test
        //public ActionResult Index()
        //{
        //    int registrationId = registrationManager.GetActiveUserId();
        //    BasicInfoViewModel basicInfoViewModel = testManager.GetBasicInfo(registrationId);
        //    return View(basicInfoViewModel);
        //}

        //[HttpPost]
        //public ActionResult Index(BasicInfoViewModel basicInfoViewModel)
        //{
        //    int registrationId = registrationManager.GetActiveUserId();
        //    basicInfoViewModel.RegistrationId = registrationId;

        //    testManager.SaveBasicInfo(basicInfoViewModel);

        //    BasicInfoViewModel newbasicInfoViewModel = testManager.GetBasicInfo(registrationId);
        //    return View(newbasicInfoViewModel);
        //}
    }
}