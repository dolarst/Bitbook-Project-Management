using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using BitbookV2.Models;

namespace BitbookV2.Context
{
    public class BitbookDbContext : DbContext
    {
        public DbSet<Registration> Registrations { set; get; }
        public DbSet<BasicInfoViewModel> BasicInfoViewModels { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Like> Likes { set; get; }
        public DbSet<Image> Images { set; get; }
        public DbSet<FriendRelation> FriendRelations { set; get; }
        public DbSet<Notification> Notifications { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Post>()
                .HasRequired(m => m.PostOwnedUser)
                .WithMany(m => m.PostOwned)
                .HasForeignKey(m => m.PostOwnedUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasRequired(m => m.PostSharedUser)
                .WithMany(m => m.PostShared)
                .HasForeignKey(m => m.PostSharedUserId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<FriendRelation>().HasKey(f => new { f.UserId, f.FriendId });
            modelBuilder.Entity<FriendRelation>()
               .HasRequired(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .WillCascadeOnDelete(true);
            modelBuilder.Entity<FriendRelation>()
                        .HasRequired(p => p.Friend)
                        .WithMany()
                        .HasForeignKey(p => p.FriendId)
                        .WillCascadeOnDelete(false);


            modelBuilder.Entity<Notification>()
              .HasRequired(p => p.Sender)
              .WithMany()
              .HasForeignKey(p => p.SenderId)
              .WillCascadeOnDelete(true);
            modelBuilder.Entity<Notification>()
                        .HasRequired(p => p.Receiver)
                        .WithMany()
                        .HasForeignKey(p => p.ReceiverId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>()
                     .HasOptional(p => p.Post)
                     .WithMany()
                     .HasForeignKey(p => p.PostId)
                     .WillCascadeOnDelete(true);

            modelBuilder.Entity<Image>()
                        .HasRequired(p => p.Registration)
                        .WithMany()
                        .HasForeignKey(p => p.RegistrationId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Image>()
                     .HasRequired(p => p.Post)
                     .WithMany()
                     .HasForeignKey(p => p.PostId)
                     .WillCascadeOnDelete(true);


            //modelBuilder.Entity<BasicInfoViewModel>()
            // .HasOptional(m => m.ProfilePic)
            // .WithMany()
            // .HasForeignKey(m => m.ProfilePicId)
            // .WillCascadeOnDelete(false);

            //modelBuilder.Entity<BasicInfoViewModel>()
            //    .HasOptional(m => m.CoverPic)
            //    .WithMany()
            //    .HasForeignKey(m => m.CoverPicId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<BasicInfoViewModel>()
           .HasRequired(m => m.User)
           .WithMany()
           .HasForeignKey(m => m.UserId)
           .WillCascadeOnDelete(false);


        }
    }
}