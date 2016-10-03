using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitbookV2.Context;
using BitbookV2.Models;

namespace BitbookV2.Bitbook.core.DAL
{
    public class FriendshipGateway
    {
        private BitbookDbContext db;

        public List<FriendRelation> GetFriendRelations(int activeUserId, int profileId)
        {
            db=new BitbookDbContext();

            var friendRelation1 = db.FriendRelations.Where(a => a.UserId == activeUserId && a.FriendId == profileId).FirstOrDefault();
            var friendRelation2 = db.FriendRelations.Where(a => a.UserId == profileId && a.FriendId == activeUserId).FirstOrDefault();

            var friendRelations = new List<FriendRelation>();
            friendRelations.Add(friendRelation1);
            friendRelations.Add(friendRelation2);

            return friendRelations;

        }

        public void AddFriend(int activeUserId, int profileId)
        {
            db=new BitbookDbContext();

            var friendRelation1 = new FriendRelation() {UserId = activeUserId,FriendId = profileId,Created = 0};
            var friendRelation2 = new FriendRelation() {UserId =profileId,FriendId = activeUserId,Created = 1};

            db.FriendRelations.Add(friendRelation1);
            db.SaveChanges();
            db.FriendRelations.Add(friendRelation2);
            db.SaveChanges();

        }

        public void ConfirmRequest(int activeUserId, int profileId)
        {
            db=new BitbookDbContext();

            var user = db.FriendRelations.Where(a => a.UserId == activeUserId && a.FriendId == profileId).FirstOrDefault();
            user.Created = 2;
            db.SaveChanges();

            var user2 = db.FriendRelations.Where(a => a.UserId == profileId && a.FriendId == activeUserId).FirstOrDefault();
            user2.Created = 2;
            db.SaveChanges();
        }

        public void DropFriendRelation(int activeUserId,int profileId)
        {
            db = new BitbookDbContext();

            var user = db.FriendRelations.Where(a => a.UserId == activeUserId && a.FriendId == profileId).FirstOrDefault();

            db.FriendRelations.Remove(user);
            db.SaveChanges();

            var user2 = db.FriendRelations.Where(a => a.UserId == profileId && a.FriendId == activeUserId).FirstOrDefault();

            db.FriendRelations.Remove(user2);
            db.SaveChanges();
        }

        public List<FriendRelation> GetFriendListByProfileId(int profileId)
        {
            db = new BitbookDbContext();

            var friendList =
                db.FriendRelations.Where(a => a.UserId == profileId && a.Created == 2 && a.FriendId != profileId).ToList();



            return friendList;
        }

        public List<FriendRelation> GetFriendRequestListByProfileId(int activeUserId)
        {
            db = new BitbookDbContext();

            var friendList =
                db.FriendRelations.Where(a => a.UserId == activeUserId && a.Created == 1 && a.FriendId != activeUserId).ToList();



            return friendList;
        } 
    }
}