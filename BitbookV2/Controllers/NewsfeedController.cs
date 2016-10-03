using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using BitbookV2.Bitbook.core.BLL;
using BitbookV2.Bitbook.core.DAL;
using BitbookV2.Custom.Attributes;
using BitbookV2.Models;


namespace BitbookV2.Controllers
{

    [NoCache]
    [Authorize]
    
    public class NewsfeedController : Controller
    {
        // GET: Newsfeed
       PostManager postManager = new PostManager();
       RegistrationManager registrationManager = new RegistrationManager();

    //[Authorize]
        public ActionResult NewsfeedHome()
        {
            
          
            return View(NewsfeedLoadComponents());
        }

       //[Authorize]
        public ActionResult LogOut()
        {
            Session["Register"] = null;
      
            FormsAuthentication.SignOut();
            return RedirectToAction("RegistrationLogin", "RegistrationLogin");
        }

        public JsonResult GetUserListByPartialText(string text)
        {
            List<Registration> searchViewModels = registrationManager.GetUserListByPartialText(text);
            string html = "";

            foreach (var searchViewModel in searchViewModels)
            {
               
               html = html + "<div class='display_box' align='left'><span class='name'>" + searchViewModel.FirstName + " "+searchViewModel.LastName+
                              "</span></div>";

            }
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult UploadImage(ImageViewModel model)
        //{
        //    HttpPostedFileBase file = Request.Files["ImageData"];
         
        //    int i = registrationManager.UploadImageInDataBase(file, model);
        //    if (i == 1)
        //    {
        //        return RedirectToAction("NewsfeedHome");
        //    }
        //    return View("NewsfeedHome");
        //}

        public ActionResult RetrieveImage(int id)

        {
            var image = postManager.GetImageByImageId(id);
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

        [HttpPost]
        public ActionResult SavePost([Bind(Prefix = "Post")]Post post)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            var imageData = ConvertToBytes(file);

            if (post.PostText == null && imageData.Count() == 0)
            {
                ModelState.AddModelError("", "Please make sure to enter at least a photo or a text!");


                return View("NewsfeedHome", NewsfeedLoadComponents());
            }

            int regId = registrationManager.GetActiveUserId();

            post.PostOwnedUserId = regId;
            post.PostSharedUserId = regId;


            var savedPost = postManager.SavePost(post);


            if (imageData.Count() != 0)
            {

                var image = new Image();
                image.ImageData = imageData;
                image.PostId = savedPost.Id;
                image.RegistrationId = regId;
                var savedImage = postManager.SaveImage(image);


            }


            return View("NewsfeedHome", NewsfeedLoadComponents());
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }


        [HttpPost]
        public ActionResult SaveComment(string commentText, int postId)
        {
            Comment comment = new Comment();

            comment.CommentText = commentText;
            comment.PostId = postId;
            comment.RegistrationId = registrationManager.GetActiveUserId();
            int regId = comment.RegistrationId;
            Post post = postManager.GetPostByPostId(postId);

            postManager.SaveComment(comment,post.PostOwnedUserId,post.PostSharedUserId);

            return View("NewsfeedHome",NewsfeedLoadComponents());
        }


        [HttpPost]
        public ActionResult SaveLike(int postId)
        {
            Like like = new Like();

         
            like.PostId = postId;
            like.RegistrationId = registrationManager.GetActiveUserId();
            int regId = like.RegistrationId;
            Post post = postManager.GetPostByPostId(postId);
            postManager.SaveLike(like,post.PostOwnedUserId,post.PostSharedUserId);


            return View("NewsfeedHome", NewsfeedLoadComponents());

        }

        public NewsfeedViewModel NewsfeedLoadComponents()
        {
            
            NewsfeedViewModel newsfeedViewModel = new NewsfeedViewModel();

            int regId = registrationManager.GetActiveUserId();
            newsfeedViewModel.Posts = postManager.GetAllPosts(regId);
            newsfeedViewModel.LoggedInUser = registrationManager.GetUserById(regId);
           

            foreach (var existingPost in newsfeedViewModel.Posts)
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

          
            return newsfeedViewModel;
        }

        public ActionResult DeletePost(int postId)
        {
            postManager.DeletePost(postId);

            return View("NewsfeedHome", NewsfeedLoadComponents());
        }

        [HttpPost]
        public ActionResult SearchUser(string search_text)
        {
           return RedirectToAction("SearchPageView", "SearchPage", new {text = search_text});
        }

        public ActionResult GetProfilePictureByProfileId(int id)
        {
            var image = registrationManager.GetProfilePictureByProfileId(id);

            if (image != null)
            {

                var imageData = image.ImageData;

                if (imageData != null)
                {
                    return File(imageData, "image/jpg");
                }
               
            }


            return File("../../Default pics/060410_Facebook_profile_pic_1.jpg", "image/jpg");


        }
    }
}