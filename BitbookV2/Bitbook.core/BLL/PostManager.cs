using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitbookV2.Bitbook.core.DAL;
using BitbookV2.Models;

namespace BitbookV2.Bitbook.core.BLL
{
    public class PostManager
    {
        PostGateway postGateway = new PostGateway();

        public Post SavePost(Post post)
        {
           return postGateway.SavePost(post);
        }

        public Post GetPost(int regId)
        {
            Post post = postGateway.GetPost(regId);

            return post;
        }


        public List<Post> GetPostListByUserId(int regId)
        {
            var postList = postGateway.GetPostListByUserId(regId);

            return postList;
        }

        public void SaveComment(Comment comment,int postOwnerUserId,int postSharedUserId)
        {
            postGateway.SaveComment(comment,postOwnerUserId,postSharedUserId);
        }

        public List<Comment> GetCommentListByPostId(int postId)
        {
            var commentList = postGateway.GetCommentListByPostId(postId);

            return commentList;
        }

        public void SaveLike(Like like, int postOwnerUserId, int postSharedUserId)
        {
            postGateway.SaveLike(like,postOwnerUserId,postSharedUserId);
        }


        public List<Like> GetLikeListByPostId(int postId)
        {
            var likeList = postGateway.GetLikeListByPostId(postId);

            return likeList;
        }

        public Image SaveImage(Image image)
        {
          
            return postGateway.SaveImage(image);

        }

        public Image GetImageByPost(Post post)
        {
            return postGateway.GetImageByPost(post);
        }

        public Image GetImageByPostId(int id)
        {
            return postGateway.GetImageByPostId(id);
        }

        public List<Post> GetProfilePostListByProfileId(int id)
        {
            return postGateway.GetProfilePostListByProfileId(id);
        }

        public List<Notification> GetAllActiveUserNotifications(int activeUserId)
        {
            return postGateway.GetAllActiveUserNotifications(activeUserId);
        }

        public Post GetPostByPostId(int postId)
        {
            return postGateway.GetPostByPostId(postId);
        }

        public Image GetImageByImageId(int id)
        {
            return postGateway.GetImageByImageId(id);
        }

        public void ChangeProfilePic(byte[] propic, int profileId)
        {
            postGateway.ChangeProfilePic(propic,profileId);
        }

        public void ChangeCoverPic(byte[] coverpic, int profileId)
        {
            postGateway.ChangeCoverPic(coverpic, profileId);
        }

        public void DeletePost(int postId)
        {
            postGateway.DeletePost(postId);
        
        }

        public List<Post> GetAllPosts(int activeUserId)
        {

            return postGateway.GetAllPost(activeUserId);
        } 





    }
}