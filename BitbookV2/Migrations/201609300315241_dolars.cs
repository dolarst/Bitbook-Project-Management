namespace BitbookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dolars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasicInfoViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        About = c.String(),
                        Experiences = c.String(),
                        Education = c.String(),
                        City = c.String(),
                        AreaOfInterest = c.String(),
                        Country = c.String(),
                        ContactEmail = c.String(),
                        PhoneNo = c.String(),
                        DateofBirth = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        ProfilePicId = c.Int(),
                        CoverPicId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        PasswordConfirm = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostText = c.String(),
                        PostOwnedUserId = c.Int(nullable: false),
                        PostSharedUserId = c.Int(nullable: false),
                        DateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.PostOwnedUserId)
                .ForeignKey("dbo.Registrations", t => t.PostSharedUserId)
                .Index(t => t.PostOwnedUserId)
                .Index(t => t.PostSharedUserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        PostId = c.Int(nullable: false),
                        RegistrationId = c.Int(nullable: false),
                        DateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.RegistrationId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.RegistrationId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        RegistrationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.RegistrationId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.RegistrationId);
            
            CreateTable(
                "dbo.FriendRelations",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                        Created = c.Int(nullable: false),
                        DateTime = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.UserId, t.FriendId })
                .ForeignKey("dbo.Registrations", t => t.FriendId)
                .ForeignKey("dbo.Registrations", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        PostId = c.Int(nullable: false),
                        RegistrationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Registrations", t => t.RegistrationId)
                .Index(t => t.PostId)
                .Index(t => t.RegistrationId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        PostId = c.Int(),
                        NotificationType = c.Int(nullable: false),
                        DateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Registrations", t => t.ReceiverId)
                .ForeignKey("dbo.Registrations", t => t.SenderId, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "SenderId", "dbo.Registrations");
            DropForeignKey("dbo.Notifications", "ReceiverId", "dbo.Registrations");
            DropForeignKey("dbo.Notifications", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Images", "RegistrationId", "dbo.Registrations");
            DropForeignKey("dbo.Images", "PostId", "dbo.Posts");
            DropForeignKey("dbo.FriendRelations", "UserId", "dbo.Registrations");
            DropForeignKey("dbo.FriendRelations", "FriendId", "dbo.Registrations");
            DropForeignKey("dbo.BasicInfoViewModels", "UserId", "dbo.Registrations");
            DropForeignKey("dbo.Posts", "PostSharedUserId", "dbo.Registrations");
            DropForeignKey("dbo.Posts", "PostOwnedUserId", "dbo.Registrations");
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Likes", "RegistrationId", "dbo.Registrations");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "RegistrationId", "dbo.Registrations");
            DropIndex("dbo.Notifications", new[] { "PostId" });
            DropIndex("dbo.Notifications", new[] { "ReceiverId" });
            DropIndex("dbo.Notifications", new[] { "SenderId" });
            DropIndex("dbo.Images", new[] { "RegistrationId" });
            DropIndex("dbo.Images", new[] { "PostId" });
            DropIndex("dbo.FriendRelations", new[] { "FriendId" });
            DropIndex("dbo.FriendRelations", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "RegistrationId" });
            DropIndex("dbo.Likes", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "RegistrationId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "PostSharedUserId" });
            DropIndex("dbo.Posts", new[] { "PostOwnedUserId" });
            DropIndex("dbo.BasicInfoViewModels", new[] { "UserId" });
            DropTable("dbo.Notifications");
            DropTable("dbo.Images");
            DropTable("dbo.FriendRelations");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.Registrations");
            DropTable("dbo.BasicInfoViewModels");
        }
    }
}
