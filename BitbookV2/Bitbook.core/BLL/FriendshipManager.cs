using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitbookV2.Bitbook.core.DAL;
using BitbookV2.Models;

namespace BitbookV2.Bitbook.core.BLL
{
    public class FriendshipManager
    {
        FriendshipGateway friendshipGateway = new FriendshipGateway();

        public List<FriendRelation> GeFriendRelations(int activeUserId, int profileId)
        {
            return friendshipGateway.GetFriendRelations(activeUserId, profileId);
        }

        public void AddFriend(int activeUserId, int profileId)
        {
           
            friendshipGateway.AddFriend(activeUserId,profileId);
        }

        public void ConfirmRequest(int activeUserId, int profileId)
        {
              friendshipGateway.ConfirmRequest(activeUserId,profileId);
        }

        public void DropFriendRelation(int activeUserId, int profileId)
        {
               friendshipGateway.DropFriendRelation(activeUserId,profileId);
        }

        public List<FriendRelation> GetFriendListByProfileId(int id)
        {
            return friendshipGateway.GetFriendListByProfileId(id);
        }

        public List<FriendRelation> GetFriendRequestListByProfileId(int activeUserId)
        {
            return friendshipGateway.GetFriendRequestListByProfileId(activeUserId);


        }
    }
}