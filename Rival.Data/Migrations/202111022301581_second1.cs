namespace Rival.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerOne_PlayerId = c.Int(nullable: false),
                        PlayerOne_FirstName = c.String(),
                        PlayerOne_LastName = c.String(),
                        PlayerOne_City = c.String(),
                        PlayerOne_State = c.Int(nullable: false),
                        PlayerTwo_PlayerId = c.Int(nullable: false),
                        PlayerTwo_FirstName = c.String(),
                        PlayerTwo_LastName = c.String(),
                        PlayerTwo_City = c.String(),
                        PlayerTwo_State = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        FinalScore = c.String(),
                        Court_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Court", t => t.Court_Id)
                .Index(t => t.Court_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "Court_Id", "dbo.Court");
            DropIndex("dbo.Match", new[] { "Court_Id" });
            DropTable("dbo.Match");
        }
    }
}
