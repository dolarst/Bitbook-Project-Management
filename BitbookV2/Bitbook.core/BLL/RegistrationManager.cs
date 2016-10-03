using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitbookV2.Bitbook.core.DAL;
using BitbookV2.Models;

namespace BitbookV2.Bitbook.core.BLL
{
    public class RegistrationManager
    {
        RegistrationGateway registrationGateway = new RegistrationGateway();

        public Registration RegisterUser(Registration registration)
        {
            

            return registrationGateway.RegisterUser(registration);
        }

        public bool DoesEmailAlreadyExist(string Email)
        {
            return registrationGateway.DoesEmailAlreadyExist(Email);
        }

        public List<Registration> GetUserListByPartialText(string text)
        {
            return registrationGateway.GetUserListByPartialText(text);
        }

        //public int UploadImageInDataBase(HttpPostedFileBase file, ImageViewModel imageViewModel)
        //{
        //    return registrationGateway.UploadImageInDataBase(file, imageViewModel);
        //}

        //public byte[] GetImageFromDataBase()
        //{
        //    return registrationGateway.GetImageFromDataBase();
        //}

        public int GetActiveUserId()
        {
            return registrationGateway.GetActiveUserId();
        }


        public List<Registration> GetLikedUserList(List<Like> likeList)
        {
            var userList = registrationGateway.GetLikedUserList(likeList);

            return userList;


        }

        public Registration GetUserById(int id)
        {
            return registrationGateway.GetUserById(id);
        }

        public BasicInfoViewModel GetBasicInfoViewModelbyId(int id)
        {
            return registrationGateway.GetBasicInfoViewModelbyId(id);
        }

        public void ChangeName(string firstName, string lastName, int activeUserId)
        {
            registrationGateway.ChangeName(firstName,lastName,activeUserId);

        }

        public void ChangeGender(string gender, int activeUserId)
        {
           registrationGateway.ChangeGender(gender,activeUserId);


        }

        public void ChangeAboutMe(string about, int activeUserId)
        {
           registrationGateway.ChangeAboutMe(about,activeUserId);


        }

        public void ChangeEducation(string education, int activeUserId)
        {
            registrationGateway.ChangeEducation(education,activeUserId);

        }

        public void ChangeAreaOfInterest(string areaOfInterest, int activeUserId)
        {
           registrationGateway.ChangeAreaOfInterest(areaOfInterest,activeUserId);


        }


        public void ChangeCity(string city, int activeUserId)
        {
          registrationGateway.ChangeCity(city,activeUserId);


        }


        public void ChangeCountry(string country, int activeUserId)
        {
            registrationGateway.ChangeCountry(country,activeUserId);

        }

        public void ChangeExperiences(string experience, int activeUserId)
        {
            registrationGateway.ChangeExperiences(experience,activeUserId);


        }
        public void ChangeContactEmailAddress(string email, int activeUserId)
        {
           registrationGateway.ChangeContactEmailAddress(email,activeUserId);

        }
        public void ChangePhoneNo(string phoneNo, int activeUserId)
        {
                 registrationGateway.ChangePhoneNo(phoneNo,activeUserId);


        }

        public bool IsEmailValid(string password, int activeUserId)
        {
           return registrationGateway.IsEmailValid(password, activeUserId);

        }

        public void ChangePassword(ChangePassword changePassword, int activeUserId)
        {
            registrationGateway.ChangePassword(changePassword,activeUserId);

        }

        public void ChangeDateOfBirth(DateTime dateOfBirth, int activeUserId)
        {
            registrationGateway.ChangeDateOfBirth(dateOfBirth,activeUserId);


        }

        public Image GetProfilePictureByProfileId(int profileId)
        {
           return registrationGateway.GetProfilePictureByProfileId(profileId);
        }
    }
}