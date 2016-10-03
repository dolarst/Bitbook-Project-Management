using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BitbookV2.Context;
using BitbookV2.Models;

namespace BitbookV2.Bitbook.core.DAL
{
    public class TestGateway
    {

        private BitbookDbContext db;

        //public void SaveBasicInfo(BasicInfoViewModel basicInfoViewModel)
        //{

        //    db=new BitbookDbContext();

        //    var user = db.BasicInfoViewModels.Where(a => a.RegistrationId == basicInfoViewModel.RegistrationId).FirstOrDefault();

        //    if (user == null)
        //    {
        //        db.BasicInfoViewModels.Add(basicInfoViewModel);
        //        db.SaveChanges();
        //    }

        //    else
        //    {
        //        user.About = basicInfoViewModel.About;
        //        user.Experiences = basicInfoViewModel.Experiences;
        //        user.Name = basicInfoViewModel.Name;
        //        db.SaveChanges();
        //    }


        //}

        //public BasicInfoViewModel GetBasicInfo(int regId)
        //{
        //    db = new BitbookDbContext();
        //    BasicInfoViewModel user = db.BasicInfoViewModels.Where(a => a.RegistrationId == regId).FirstOrDefault();

        //    if (user == null)
        //    {
        //      user= new BasicInfoViewModel();
        //    }

        //    return user;
        //}


    }
    }
