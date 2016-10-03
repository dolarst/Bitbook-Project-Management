using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using BitbookV2.Context;
using BitbookV2.Models;


namespace BitbookV2.Bitbook.core.DAL
{
    public class PostGateway
    {
        private BitbookDbContext db;


        public Post SavePost(Post post)
        {
            db = new BitbookDbContext();
            post.DateTime = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();

            if (post.PostOwnedUserId != post.PostSharedUserId)
            {
                NotificationType notificationType = new NotificationType();
                Notification notification = new Notification();

                notification.ReceiverId = post.PostSharedUserId;
                notification.SenderId = post.PostOwnedUserId;
                notification.NotificationType = notificationType.PostToWall;
                notification.PostId = post.Id;
                notification.DateTime = DateTime.Now;

                db.Notifications.Add(notification);
                db.SaveChanges();
            }

            return post;

        }

        public Post GetPost(int regId)
        {
            db = new BitbookDbContext();

            Post post = db.Posts.Where(a => a.PostOwnedUserId == regId).FirstOrDefault();



            return post;
        }

        public List<Post> GetPostListByUserId(int regId)
        {
            db = new BitbookDbContext();

            var post = db.Posts.Where(a => a.PostOwnedUserId == regId).ToList();

            return post;
        }

        public void SaveComment(Comment comment, int postOwnerUserId, int postSharedUserId)
        {
            db = new BitbookDbContext();
            comment.DateTime = DateTime.Now;
            db.Comments.Add(comment);

            db.SaveChanges();

            NotificationType notificationType = new NotificationType();

            Notification notification = new Notification();

            notification.PostId = comment.PostId;


            if (comment.RegistrationId == postOwnerUserId && comment.RegistrationId == postSharedUserId)
            {

            }
            if (comment.RegistrationId == postOwnerUserId && comment.RegistrationId != postSharedUserId)
            {
                notification.NotificationType = notificationType.CommentOnSharedPost;
                notification.ReceiverId = postSharedUserId;
                notification.SenderId = comment.RegistrationId;
                notification.DateTime = DateTime.Now;
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
            if (comment.RegistrationId != postOwnerUserId && comment.RegistrationId == postSharedUserId)
            {
                notification.NotificationType = notificationType.Comment;
                notification.ReceiverId = postOwnerUserId;
                notification.SenderId = comment.RegistrationId;
                notification.DateTime = DateTime.Now;

                db.Notifications.Add(notification);
                db.SaveChanges();
            }
            if (comment.RegistrationId != postOwnerUserId && comment.RegistrationId != postSharedUserId)
            {
                if (postOwnerUserId == postSharedUserId)
                {
                    notification.NotificationType = notificationType.Comment;
                    notification.ReceiverId = postOwnerUserId;
                    notification.SenderId = comment.RegistrationId;
                    notification.DateTime = DateTime.Now;

                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }
                else
                {
                    notification.NotificationType = notificationType.Comment;
                    notification.ReceiverId = postOwnerUserId;
                    notification.SenderId = comment.RegistrationId;
                    notification.DateTime = DateTime.Now;

                    db.Notifications.Add(notification);
                    db.SaveChanges();


                    notification.NotificationType = notificationType.CommentOnSharedPost;
                    notification.ReceiverId = postSharedUserId;
                    notification.SenderId = comment.RegistrationId;
                    notification.DateTime = DateTime.Now;

                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }
            }

        }

        public List<Comment> GetCommentListByPostId(int postId)
        {
            db = new BitbookDbContext();

            var commentList = db.Comments.Where(a => a.PostId == postId).OrderByDescending(c=>c.DateTime).ToList();

            return commentList;
        }

        public void SaveLike(Like like, int postOwnerUserId, int postSharedUserId)
        {
            db = new BitbookDbContext();

            var existingLike =
                db.Likes.Where(a => a.PostId == like.PostId && a.RegistrationId == like.RegistrationId).FirstOrDefault();

            if (existingLike == null)
            {
                db.Likes.Add(like);

                db.SaveChanges();

                NotificationType notificationType = new NotificationType();

                Notification notification = new Notification();

                notification.PostId = like.PostId;


                if (like.RegistrationId == postOwnerUserId && like.RegistrationId == postSharedUserId)
                {

                }
                if (like.RegistrationId == postOwnerUserId && like.RegistrationId != postSharedUserId)
                {
                    notification.NotificationType = notificationType.LikeOnSharedPost;
                    notification.ReceiverId = postSharedUserId;
                    notification.SenderId = like.RegistrationId;
                    notification.DateTime = DateTime.Now;

                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }
                if (like.RegistrationId != postOwnerUserId && like.RegistrationId == postSharedUserId)
                {
                    notification.NotificationType = notificationType.Like;
                    notification.ReceiverId = postOwnerUserId;
                    notification.SenderId = like.RegistrationId;
                    notification.DateTime = DateTime.Now;

                    db.Notifications.Add(notification);
                    db.SaveChanges();
                }
                if (like.RegistrationId != postOwnerUserId && like.RegistrationId != postSharedUserId)
                {
                    if (postOwnerUserId == postSharedUserId)
                    {
                        notification.NotificationType = notificationType.Like;
                        notification.ReceiverId = postOwnerUserId;
                        notification.SenderId = like.RegistrationId;
                        notification.DateTime = DateTime.Now;

                        db.Notifications.Add(notification);
                        db.SaveChanges();
                    }
                    else
                    {
                        notification.NotificationType = notificationType.Like;
                        notification.ReceiverId = postOwnerUserId;
                        notification.SenderId = like.RegistrationId;
                        notification.DateTime = DateTime.Now;

                        db.Notifications.Add(notification);
                        db.SaveChanges();


                        notification.NotificationType = notificationType.LikeOnSharedPost;
                        notification.ReceiverId = postSharedUserId;
                        notification.SenderId = like.RegistrationId;
                        notification.DateTime = DateTime.Now;

                        db.Notifications.Add(notification);
                        db.SaveChanges();
                    }
                }


            }
            else
            {

                db.Likes.Remove(existingLike);

                db.SaveChanges();
            }
        }

        public List<Like> GetLikeListByPostId(int postId)
        {
            db = new BitbookDbContext();

            var likeList = db.Likes.Where(a => a.PostId == postId).ToList();


            return likeList;
        }

        public Image SaveImage(Image image)
        {
            db = new BitbookDbContext();

            db.Images.Add(image);
            db.SaveChanges();


            return image;




        }

        public Image GetImageByPost(Post post)
        {
            db = new BitbookDbContext();

            var image = db.Images.Where(a => a.Id == post.Id).FirstOrDefault();

            return image;
        }

        public Image GetImageByPostId(int id)
        {
            db = new BitbookDbContext();

            var image = db.Images.Where(a => a.PostId == id).FirstOrDefault();


            return image;

        }

        public Image GetImageByImageId(int id)
        {
            db = new BitbookDbContext();

            var image = db.Images.Where(a => a.Id == id).FirstOrDefault();


            return image;

        }

        public List<Post> GetProfilePostListByProfileId(int id)
        {
            db = new BitbookDbContext();

            var postList = db.Posts.Where(a => a.PostSharedUserId == id).OrderByDescending(c=>c.DateTime).ToList();

            return postList;
        }

        public List<Post> GetAllPost(int userId)
        {
            db = new BitbookDbContext();

            var friendIdList =
                db.FriendRelations.Where(c => c.UserId == userId && c.Created == 2).Select(d => d.FriendId).ToList();

            var posts = db.Posts.Where(c => c.PostOwnedUserId == userId || friendIdList.Contains(c.PostOwnedUserId)).OrderByDescending(c=>c.DateTime);
       
            return posts.ToList();
        }

        public List<Notification> GetAllActiveUserNotifications(int activeUserId)
        {
            db = new BitbookDbContext();

            var notifications = db.Notifications.Where(a => a.ReceiverId == activeUserId).OrderByDescending(c=>c.DateTime).ToList();

            return notifications;

        }

        public Post GetPostByPostId(int postId)
        {
            db = new BitbookDbContext();

            var post = db.Posts.Where(a => a.Id == postId).FirstOrDefault();

            return post;
        }

        public void ChangeProfilePic(byte[] propic, int profileId)
        {
            db = new BitbookDbContext();
            var post = new Post();
            post.PostSharedUserId = profileId;
            post.PostOwnedUserId = profileId;
            post.DateTime = DateTime.Now;

            post.PostText = "has changed his Profile Picture.";

            db.Posts.Add(post);
            db.SaveChanges();

            var image = new Image();
            image.ImageData = propic;
            image.RegistrationId = profileId;
            image.PostId = post.Id;
            db.Images.Add(image);
            db.SaveChanges();


            var basicinfo = db.BasicInfoViewModels.Where(a => a.UserId == profileId).FirstOrDefault();

            basicinfo.ProfilePicId = image.Id;
            db.SaveChanges();

            var friendList =
               db.FriendRelations.Where(a => a.UserId == profileId && a.Created == 2 && a.FriendId != profileId).ToList();


            var notifications = new List<Notification>();
            NotificationType notificationType = new NotificationType();

            foreach (var friendRelation in friendList)
            {
                Notification notification = new Notification();
                notification.PostId = post.Id;
                notification.SenderId = profileId;
                notification.ReceiverId = friendRelation.FriendId;
                notification.NotificationType = notificationType.ChangeProfilePicture;
                notification.DateTime = DateTime.Now;
                notifications.Add(notification);
            }

            db.Notifications.AddRange(notifications);
            db.SaveChanges();


        }

        public void ChangeCoverPic(byte[] coverpic, int profileId)
        {
            db = new BitbookDbContext();
            var post = new Post();
            post.PostSharedUserId = profileId;
            post.PostOwnedUserId = profileId;
            post.DateTime = DateTime.Now;


            post.PostText = "has changed his Cover Picture.";

            db.Posts.Add(post);
            db.SaveChanges();

            var image = new Image();
            image.ImageData = coverpic;
            image.RegistrationId = profileId;
            image.PostId = post.Id;
            db.Images.Add(image);
            db.SaveChanges();


            var basicinfo = db.BasicInfoViewModels.Where(a => a.UserId == profileId).FirstOrDefault();

            basicinfo.CoverPicId = image.Id;
            db.SaveChanges();

            var friendList =
              db.FriendRelations.Where(a => a.UserId == profileId && a.Created == 2 && a.FriendId != profileId).ToList();


            var notifications = new List<Notification>();
            NotificationType notificationType = new NotificationType();

            foreach (var friendRelation in friendList)
            {
                Notification notification = new Notification();
                notification.PostId = post.Id;
                notification.SenderId = profileId;
                notification.ReceiverId = friendRelation.FriendId;
                notification.NotificationType = notificationType.ChangeCoverPicture;
                notification.DateTime = DateTime.Now;
                notifications.Add(notification);
            }

            db.Notifications.AddRange(notifications);
            db.SaveChanges();



        }

        public void DeletePost(int postId)
        {          
              db = new BitbookDbContext();

            var post = db.Posts.Where(a => a.Id == postId).FirstOrDefault();

            if (post.PostOwnedUserId == post.PostSharedUserId)
            {
                var basicinfo = db.BasicInfoViewModels.Where(a => a.UserId == post.PostOwnedUserId).FirstOrDefault();
                var image = GetImageByPostId(post.Id);

                if (image == null)
                {
                    var post2 = db.Posts.Where(a => a.Id == postId).FirstOrDefault();
                    db.Posts.Remove(post2);
                    db.SaveChanges();
                    return;
                }

                if (basicinfo.ProfilePicId == image.Id)
                {
                    var basicinfo2 = db.BasicInfoViewModels.Where(a => a.UserId == post.PostOwnedUserId).FirstOrDefault();
                    basicinfo2.ProfilePicId = 0;
                    db.SaveChanges();
               
                }
                if (basicinfo.CoverPicId == image.Id)
                {
                    var basicinfo3 = db.BasicInfoViewModels.Where(a => a.UserId == post.PostOwnedUserId).FirstOrDefault();
                    basicinfo3.CoverPicId = 0;
                    db.SaveChanges();
                }

                var post3 = db.Posts.Where(a => a.Id == postId).FirstOrDefault();
                db.Posts.Remove(post3);
           
                db.SaveChanges();
                return;

            }

            db.Posts.Remove(post);
            db.SaveChanges();

        }
    
    
    
    
    
    
    
    
    }





}