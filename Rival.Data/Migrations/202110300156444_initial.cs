namespace Rival.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match", "Player1_Id", "dbo.Player");
            DropForeignKey("dbo.Match", "Player2_Id", "dbo.Player");
            DropIndex("dbo.Match", new[] { "Player1_Id" });
            DropIndex("dbo.Match", new[] { "Player2_Id" });
            DropPrimaryKey("dbo.Player");
            AddColumn("dbo.Player", "PlayerId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Player", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Player", "City", c => c.String(nullable: false));
            AddColumn("dbo.Player", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.Player", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Player", "SportsmanshipRating", c => c.Int());
            AddPrimaryKey("dbo.Player", "PlayerId");
            DropColumn("dbo.Match", "Player1_Id");
            DropColumn("dbo.Match", "Player2_Id");
            DropColumn("dbo.Player", "Id");
            DropColumn("dbo.Player", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "Location", c => c.String());
            AddColumn("dbo.Player", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Match", "Player2_Id", c => c.Int());
            AddColumn("dbo.Match", "Player1_Id", c => c.Int());
            DropPrimaryKey("dbo.Player");
            AlterColumn("dbo.Player", "SportsmanshipRating", c => c.Int(nullable: false));
            AlterColumn("dbo.Player", "LastName", c => c.String());
            DropColumn("dbo.Player", "State");
            DropColumn("dbo.Player", "City");
            DropColumn("dbo.Player", "UserId");
            DropColumn("dbo.Player", "PlayerId");
            AddPrimaryKey("dbo.Player", "Id");
            CreateIndex("dbo.Match", "Player2_Id");
            CreateIndex("dbo.Match", "Player1_Id");
            AddForeignKey("dbo.Match", "Player2_Id", "dbo.Player", "Id");
            AddForeignKey("dbo.Match", "Player1_Id", "dbo.Player", "Id");
        }
    }
}
