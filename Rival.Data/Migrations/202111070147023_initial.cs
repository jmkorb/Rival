namespace Rival.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Court",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Condition = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        FinalScore = c.String(),
                        Court_Id = c.Int(),
                        PlayerOne_MatchPlayerId = c.Int(nullable: false),
                        PlayerTwo_MatchPlayerId = c.Int(nullable: false),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Court", t => t.Court_Id)
                .ForeignKey("dbo.MatchPlayerRecord", t => t.PlayerOne_MatchPlayerId, cascadeDelete: true)
                .ForeignKey("dbo.MatchPlayerRecord", t => t.PlayerTwo_MatchPlayerId, cascadeDelete: true)
                .ForeignKey("dbo.MatchPlayer", t => t.Winner_Id)
                .Index(t => t.Court_Id)
                .Index(t => t.PlayerOne_MatchPlayerId)
                .Index(t => t.PlayerTwo_MatchPlayerId)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.MatchPlayerRecord",
                c => new
                    {
                        MatchPlayerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.MatchPlayerId);
            
            CreateTable(
                "dbo.MatchPlayer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateJoined = c.DateTime(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Availability = c.Int(nullable: false),
                        PreferredSetNumber = c.Int(nullable: false),
                        SportsmanshipRating = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateJoined = c.DateTime(nullable: false),
                        City = c.String(nullable: false),
                        State = c.Int(nullable: false),
                        Availability = c.Int(nullable: false),
                        PreferredSetNumber = c.Int(nullable: false),
                        SportsmanshipRating = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Match", "Winner_Id", "dbo.MatchPlayer");
            DropForeignKey("dbo.Match", "PlayerTwo_MatchPlayerId", "dbo.MatchPlayerRecord");
            DropForeignKey("dbo.Match", "PlayerOne_MatchPlayerId", "dbo.MatchPlayerRecord");
            DropForeignKey("dbo.Match", "Court_Id", "dbo.Court");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Match", new[] { "Winner_Id" });
            DropIndex("dbo.Match", new[] { "PlayerTwo_MatchPlayerId" });
            DropIndex("dbo.Match", new[] { "PlayerOne_MatchPlayerId" });
            DropIndex("dbo.Match", new[] { "Court_Id" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Player");
            DropTable("dbo.MatchPlayer");
            DropTable("dbo.MatchPlayerRecord");
            DropTable("dbo.Match");
            DropTable("dbo.Court");
        }
    }
}
