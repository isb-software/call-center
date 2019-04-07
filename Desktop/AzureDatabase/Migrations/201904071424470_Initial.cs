namespace AzureDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Description, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CallCounts", "StatusId", "dbo.Status");
            DropIndex("dbo.Status", new[] { "Description" });
            DropIndex("dbo.CallCounts", new[] { "StatusId" });
            DropIndex("dbo.CallCounts", new[] { "Date" });
            DropTable("dbo.Status");
            DropTable("dbo.CallCounts");
        }
    }
}
