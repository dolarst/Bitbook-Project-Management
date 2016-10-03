using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Security;
using System.Web.UI;
using BitbookV2.Bitbook.core.BLL;
using BitbookV2.Context;
using BitbookV2.Models;

namespace BitbookV2.Bitbook.core.DAL
{
    public class RegistrationGateway
    {
        private BitbookDbContext db;
    
        public Registration RegisterUser(Models.Registration registration)
        {
            if (registration != null)
            {


               db = new BitbookDbContext();

                db.Registrations.Add(registration);

                int rowsAffected = db.SaveChanges();

            

              

                FriendRelation friend = new FriendRelation();
                friend.UserId = registration.Id;
                friend.FriendId = registration.Id;
                friend.Created = 2;
                db.FriendRelations.Add(friend);
                db.SaveChanges();

                BasicInfoViewModel basicInfoViewModel = new BasicInfoViewModel();
                basicInfoViewModel.Gender = registration.Gender;
                basicInfoViewModel.ContactEmail = registration.Email;
                basicInfoViewModel.UserId = registration.Id;
                basicInfoViewModel.DateofBirth = registration.BirthDate;

                db.BasicInfoViewModels.Add(basicInfoViewModel);
                db.SaveChanges();


                return registration;


            }

            return null;
        }

        public bool DoesEmailAlreadyExist(string Email)
        {
            db=new BitbookDbContext();
            return db.Registrations.Any(a => a.Email.Equals(Email));
        }

        public List<Registration> GetUserListByPartialText(string text)
        {
            db = new BitbookDbContext();

            var registrations =
                db.Registrations.Where(a => (a.FirstName + " " + a.LastName).Contains(text)).ToList();


           
            return registrations;
        }

        public int GetActiveUserId()
        {
            db = new BitbookDbContext();

            var user = db.Registrations.Where(a => a.Email == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            return user.Id;
        }

        public List<Registration> GetLikedUserList(List<Like> likeList )
        {
            db=new BitbookDbContext();

            var userList = new List<Registration>();
            foreach (var like in likeList)
            {
                var registration = db.Registrations.Where(a => a.Id == like.RegistrationId).FirstOrDefault();
                userList.Add(registration);
            }
            return userList;
        }

        public Registration GetUserById(int id)
        {
            db =new BitbookDbContext();

            var user = db.Registrations.Where(a => a.Id == id).FirstOrDefault();

            return user;
        }

        public BasicInfoViewModel GetBasicInfoViewModelbyId(int id )
        {
            db = new BitbookDbContext();

            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId== id).FirstOrDefault();

            return basicInfo;
        }

        public void ChangeName(string firstName, string lastName, int activeUserId)
        {
            db= new BitbookDbContext();

            var user = db.Registrations.Where(a => a.Id == activeUserId).FirstOrDefault();
            user.FirstName = firstName;
            user.LastName = lastName;
            db.SaveChanges();

            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.Name = firstName + " " + lastName;
            db.SaveChanges();



        }

        public void ChangeGender(string gender,int activeUserId)
        {
            db=new BitbookDbContext();

            var user = db.Registrations.Where(a => a.Id == activeUserId).FirstOrDefault();
            user.Gender = gender;
            db.SaveChanges();

            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.Gender = gender;
            db.SaveChanges();


        }

        public void ChangeAboutMe(string about, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.About = about;
            db.SaveChanges();


        }

        public void ChangeEducation(string education, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.Education = education;
            db.SaveChanges();


        }

        public void ChangeAreaOfInterest(string areaOfInterest, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.AreaOfInterest = areaOfInterest;
            db.SaveChanges();


        }


        public void ChangeCity(string city, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.City = city;
            db.SaveChanges();


        }


        public void ChangeCountry(string country, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.Country= country;
            db.SaveChanges();


        }

        public void ChangeExperiences(string experience, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.Experiences = experience;
            db.SaveChanges();


        }
        public void ChangeContactEmailAddress(string email, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.ContactEmail = email;
            db.SaveChanges();


        }
        public void ChangePhoneNo(string phoneNo, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.PhoneNo = phoneNo;
            db.SaveChanges();


        }

        public bool IsEmailValid(string password, int activeUserId)
        {
            db=new BitbookDbContext();
            var user = db.Registrations.Where(a => a.Id == activeUserId).FirstOrDefault();

            if (user.Password == password)
            {
                return true;
            }
            return false;
        }

        public void ChangePassword(ChangePassword changePassword, int activeUserId)
        {
            db=new BitbookDbContext();
            var user = db.Registrations.Where(a => a.Id == activeUserId).FirstOrDefault();
            user.Password = changePassword.NewPassword;
            user.PasswordConfirm = changePassword.NewPassword;
            db.SaveChanges();


        }

        public void ChangeDateOfBirth(DateTime dateOfBirth, int activeUserId)
        {
            db = new BitbookDbContext();


            var basicInfo = db.BasicInfoViewModels.Where(a => a.UserId == activeUserId).FirstOrDefault();
            basicInfo.DateofBirth = dateOfBirth;
            db.SaveChanges();

            var user = db.Registrations.Where(a => a.Id == activeUserId).FirstOrDefault();
            user.BirthDate = dateOfBirth;
            db.SaveChanges();


        }


        public Image GetProfilePictureByProfileId(int profileId)
        {
            db=new BitbookDbContext();
            var basicinfo = db.BasicInfoViewModels.Where(a => a.UserId == profileId).FirstOrDefault();

         
              var  image = db.Images.Where(a => a.Id == basicinfo.ProfilePicId).FirstOrDefault();



            return image;
        }

    }
}