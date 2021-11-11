namespace Rival.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removematchplayer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "Winner_Id", "dbo.MatchPlayer");
            DropForeignKey("dbo.Match", "Court_Id", "dbo.Court");
            DropIndex("dbo.Match", new[] { "Court_Id" });
            DropIndex("dbo.Match", new[] { "Winner_Id" });
            RenameColumn(table: "dbo.Match", name: "Court_Id", newName: "CourtId");
            AddColumn("dbo.Match", "Winner", c => c.String());
            AlterColumn("dbo.Match", "CourtId", c => c.Int(nullable: false));
            CreateIndex("dbo.Match", "CourtId");
            AddForeignKey("dbo.Match", "CourtId", "dbo.Court", "Id", cascadeDelete: true);
            DropColumn("dbo.Match", "Winner_Id");
            DropTable("dbo.MatchPlayer");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Match", "Winner_Id", c => c.Int());
            DropForeignKey("dbo.Match", "CourtId", "dbo.Court");
            DropIndex("dbo.Match", new[] { "CourtId" });
            AlterColumn("dbo.Match", "CourtId", c => c.Int());
            DropColumn("dbo.Match", "Winner");
            RenameColumn(table: "dbo.Match", name: "CourtId", newName: "Court_Id");
            CreateIndex("dbo.Match", "Winner_Id");
            CreateIndex("dbo.Match", "Court_Id");
            AddForeignKey("dbo.Match", "Court_Id", "dbo.Court", "Id");
            AddForeignKey("dbo.Match", "Winner_Id", "dbo.MatchPlayer", "Id");
        }
    }
}
