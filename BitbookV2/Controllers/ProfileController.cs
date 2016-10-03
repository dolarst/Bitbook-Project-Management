using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BitbookV2.Bitbook.core.BLL;
using BitbookV2.Custom.Attributes;
using BitbookV2.Models;

namespace BitbookV2.Controllers
{


    [NoCache]
    [Authorize]
    public class ProfileController : Controller
    {
        RegistrationManager registrationManager = new RegistrationManager();
        PostManager postManager = new PostManager();
        FriendshipManager friendshipManager = new FriendshipManager();
        private ProfilePageViewModel profilePageViewModel;


        // GET: Profile
        public ActionResult ProfilePage(int id)
        {
            ProfilePageViewModel profilePageViewModel = ProfilePageLoadComponents(id);
            return View(profilePageViewModel);
        }


        public JsonResult GetUserListByPartialText(string text)
        {
            List<Registration> searchViewModels = registrationManager.GetUserListByPartialText(text);
            string html = "";

            foreach (var searchViewModel in searchViewModels)
            {

                html = html + "<div class='display_box' align='left'><span class='name'>" + searchViewModel.FirstName +" " + searchViewModel.LastName+
                               "</span></div>";

            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            Session["Register"] = null;

            FormsAuthentication.SignOut();
            return RedirectToAction("RegistrationLogin", "RegistrationLogin");
        }

        public ActionResult RetrieveImage(int id)
        {
            var image = postManager.GetImageByImageId(id);
            if (image != null)
            {
                var imageData = image.ImageData;

                if (imageData != null)
                {
                    return File(imageData, "image/jpg");
                }
            }

            return null;
            
        }

        [HttpPost]
        public ActionResult SavePost([Bind(Prefix = "Post")]Post post, int postSharedUserId)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            var imageData = ConvertToBytes(file);

            if (post.PostText == null && imageData.Count() == 0)
            {
                ModelState.AddModelError("", "Please make sure to enter at least a photo or a text!");


                return View("ProfilePage", ProfilePageLoadComponents(postSharedUserId));
            }

            int regId = registrationManager.GetActiveUserId();

            post.PostOwnedUserId = regId;
            post.PostSharedUserId = postSharedUserId;
          

           var savedPost= postManager.SavePost(post);
          

            if (imageData.Count() != 0)
            {

                var image = new Image();
                image.ImageData = imageData;
                image.PostId = savedPost.Id;
                image.RegistrationId = regId;
                var savedImage = postManager.SaveImage(image);


            }


            return View("ProfilePage", ProfilePageLoadComponents(post.PostSharedUserId));
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }


        [HttpPost]
        public ActionResult SaveComment(string commentText, int postId, int profileId)
        {
            Comment comment = new Comment();

            comment.CommentText = commentText;
            comment.PostId = postId;
            comment.RegistrationId = registrationManager.GetActiveUserId();
            int regId = comment.RegistrationId;
            Post post = postManager.GetPostByPostId(postId);

            postManager.SaveComment(comment,post.PostOwnedUserId,post.PostSharedUserId);

            return View("ProfilePage", ProfilePageLoadComponents(profileId));
        }


        [HttpPost]
        public ActionResult SaveLike(int postId, int profileId)
        {
            Like like = new Like();


            like.PostId = postId;
            like.RegistrationId = registrationManager.GetActiveUserId();
            Post post = postManager.GetPostByPostId(postId);


            postManager.SaveLike(like,post.PostOwnedUserId,post.PostSharedUserId);
           


            return View("ProfilePage", ProfilePageLoadComponents(profileId));

        }

        public int FriendshipStatus(int id)
        {
            int activeUserId = registrationManager.GetActiveUserId();

            int profileId = id;

            var friendRelations = friendshipManager.GeFriendRelations(activeUserId, profileId);

            if (friendRelations[1]!=null && friendRelations[0]!=null)
            {
                if (friendRelations[0].UserId==activeUserId)
                {
                    if (friendRelations[0].Created==2)
                    {
                        return 3;
                    }
                    else if (friendRelations[0].Created==0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                    
                }
                else if (friendRelations[1].UserId==activeUserId)
                {
                    if (friendRelations[1].Created == 2)
                    {
                        return 3;
                    }
                    else if (friendRelations[1].Created == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                    
                }
               
            }
          
            
                return 0;
            
        }

        
        public ActionResult AddFriend(int profileId)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            friendshipManager.AddFriend(activeUserId,profileId);

            return View("ProfilePage", ProfilePageLoadComponents(profileId));
        }

        public ActionResult ConfirmRequest(int profileId)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            friendshipManager.ConfirmRequest(activeUserId, profileId);

            return View("ProfilePage", ProfilePageLoadComponents(profileId));
        }

        public ActionResult DropFriendRelation(int profileId)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            friendshipManager.DropFriendRelation(activeUserId, profileId);

            return View("ProfilePage", ProfilePageLoadComponents(profileId));
        }

        public ProfilePageViewModel ProfilePageLoadComponents(int id)
        {
            int activeUserId = registrationManager.GetActiveUserId();

            profilePageViewModel = new ProfilePageViewModel();
            profilePageViewModel.Registration = registrationManager.GetUserById(id);
            profilePageViewModel.BasicInfoViewModel= registrationManager.GetBasicInfoViewModelbyId(id);
            profilePageViewModel.FriendList = friendshipManager.GetFriendListByProfileId(id);
            profilePageViewModel.FriendRequestList = friendshipManager.GetFriendRequestListByProfileId(id);

            profilePageViewModel.FriendshipStatus = FriendshipStatus(id);


            profilePageViewModel.PostList = postManager.GetProfilePostListByProfileId(id);


            foreach (var existingPost in profilePageViewModel.PostList)
            {
                existingPost.Comments = postManager.GetCommentListByPostId(existingPost.Id);

                foreach (var comment in existingPost.Comments)
                {
                    comment.Registration = registrationManager.GetUserById(comment.RegistrationId);
                }
                existingPost.Likes = postManager.GetLikeListByPostId(existingPost.Id);
                var likedUserList = registrationManager.GetLikedUserList(existingPost.Likes);
                existingPost.LikedUserList = likedUserList;

                existingPost.PostOwnedUser = registrationManager.GetUserById(existingPost.PostOwnedUserId);
                existingPost.PostSharedUser = registrationManager.GetUserById(existingPost.PostSharedUserId);

                existingPost.Image = postManager.GetImageByPostId(existingPost.Id);
            }


          
            profilePageViewModel.LoggedInUser = registrationManager.GetUserById(activeUserId);

            profilePageViewModel.Notifications = postManager.GetAllActiveUserNotifications(activeUserId);

            return profilePageViewModel;

        }

        public ActionResult FriendListPage(int profileId)
        {
           return null;
        }

       [HttpPost]
        public ActionResult ChangeProfilePic()
       {
           int profileId = registrationManager.GetActiveUserId();

            HttpPostedFileBase file = Request.Files["ImageData"];
            var imageData = ConvertToBytes(file);

            postManager.ChangeProfilePic(imageData,profileId);

            return View("ProfilePage", ProfilePageLoadComponents(profileId));
        }

       [HttpPost]
       public ActionResult ChangeCoverPic()
       {
           int profileId = registrationManager.GetActiveUserId();

           HttpPostedFileBase file = Request.Files["ImageData"];
           var imageData = ConvertToBytes(file);

           postManager.ChangeCoverPic(imageData, profileId);

           return View("ProfilePage", ProfilePageLoadComponents(profileId));
       }


        public ActionResult DeletePost(int postId, int profileId)
        {
            
            postManager.DeletePost(postId);

            return View("ProfilePage", ProfilePageLoadComponents(profileId));



        }

        [HttpPost]
        public ActionResult ChangeName(ChangeName changeName)
        {
            int activeUserId = registrationManager.GetActiveUserId();

            if (ModelState.IsValid)
            {

                registrationManager.ChangeName(changeName.FirstName, changeName.LastName, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeGender(ChangeGender changeGender)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeGender(changeGender.Gender, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeEducation(ChangeEducation changeEducation)
        {
            int activeUserId = registrationManager.GetActiveUserId();

            registrationManager.ChangeEducation(changeEducation.Education, activeUserId);
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeAboutMe(ChangeAboutMe changeAboutMe)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeAboutMe(changeAboutMe.AboutMe, activeUserId);

            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeCity(ChangeCity changeCity)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeCity(changeCity.City, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeCountry(ChangeCountry changeCountry)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeCountry(changeCountry.Country, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeContactEmail(ChangeEmail changeEmail)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeContactEmailAddress(changeEmail.Email, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangePhoneNo(ChangePhoneNo changePhoneNo)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangePhoneNo(changePhoneNo.PhoneNo, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeExperiences(ChangeExperiences changeExperiences)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeExperiences(changeExperiences.Experiences, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        [HttpPost]
        public ActionResult ChangeAreaOfInterest(ChangeAreaOfInterest changeAreaOfInterest)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeAreaOfInterest(changeAreaOfInterest.AreaOfInterest, activeUserId);
            }

            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }


        public JsonResult IsPasswordValid([Bind(Prefix = "ChangePassword.Password")]string password)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            bool exists = registrationManager.IsEmailValid(password, activeUserId);
            return Json(exists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            
            if (ModelState.IsValid)
            {
                
                registrationManager.ChangePassword(changePassword,activeUserId);
            }

          

            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }


        [HttpPost]
        public ActionResult SearchUser(string search_text)
        {
            return RedirectToAction("SearchPageView", "SearchPage", new { text = search_text });
        }


        [HttpPost]
        public ActionResult ChangeDateofBirth(ChangeDateOfBirth changeDateOfBirth)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            if (ModelState.IsValid)
            {
                registrationManager.ChangeDateOfBirth(changeDateOfBirth.DateOfBirth, activeUserId);
            }
            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
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


        public ActionResult AddFriendFromList(int profileId)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            friendshipManager.AddFriend(activeUserId, profileId);

            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        public ActionResult ConfirmRequestFromList(int profileId)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            friendshipManager.ConfirmRequest(activeUserId, profileId);

            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }

        public ActionResult DropFriendRelationFromList(int profileId)
        {
            int activeUserId = registrationManager.GetActiveUserId();
            friendshipManager.DropFriendRelation(activeUserId, profileId);

            return View("ProfilePage", ProfilePageLoadComponents(activeUserId));
        }





    }
}