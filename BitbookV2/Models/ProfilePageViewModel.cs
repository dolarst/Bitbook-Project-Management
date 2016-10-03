using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class ProfilePageViewModel
    {
        public Registration Registration;

        public List<Post> PostList { set; get; }

        public Post Post { set; get; }

        public BasicInfoViewModel BasicInfoViewModel { set; get; }

        public List<FriendRelation> FriendList { set; get; }

        public List<FriendRelation> FriendRequestList { set; get; }

        public Registration LoggedInUser { set; get; }

        public int FriendshipStatus { set; get; }

        public List<Notification> Notifications { set; get; }

        public ChangePassword ChangePassword { set; get; }

        public ChangeName ChangeName { set; get; }

        public ChangeGender ChangeGender { set; get; }
        public ChangeDateOfBirth ChangeDateOfBirth { set; get; }
        public ChangeAboutMe ChangeAboutMe { set; get; }
        public ChangeEducation ChangeEducation { set; get; }
        public ChangeExperiences ChangeExperiences { set; get; }
        public ChangeAreaOfInterest ChangeAreaOfInterest { set; get; }
        public ChangeCity ChangeCity { set; get; }
        public ChangeCountry ChangeCountry { set; get; }
        public ChangeEmail ChangeEmail { set; get; }
        public ChangePhoneNo ChangePhoneNo { set; get; }
    }
}