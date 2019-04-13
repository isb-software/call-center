namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCallCounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CallCounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.Date)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CallCounts", "StatusId", "dbo.Status");
            DropIndex("dbo.CallCounts", new[] { "StatusId" });
            DropIndex("dbo.CallCounts", new[] { "Date" });
            DropTable("dbo.CallCounts");
        }
    }
}
