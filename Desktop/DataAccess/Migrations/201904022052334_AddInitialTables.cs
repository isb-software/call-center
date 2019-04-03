namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InboundCalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        DateTimeOfCall = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        RecordingPath = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.DateTimeOfCall);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Description, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.FirstName, t.LastName }, unique: true, name: "FullName");
            
            CreateTable(
                "dbo.NormalQueues",
                c => new
                    {
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.PhoneNumber);
            
            CreateTable(
                "dbo.OutboundCalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        DateTimeOfCall = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        RecordingPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.DateTimeOfCall);
            
            CreateTable(
                "dbo.PriorityQueues",
                c => new
                    {
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.PhoneNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OutboundCalls", "UserId", "dbo.Users");
            DropForeignKey("dbo.OutboundCalls", "StatusId", "dbo.Status");
            DropForeignKey("dbo.InboundCalls", "UserId", "dbo.Users");
            DropForeignKey("dbo.InboundCalls", "StatusId", "dbo.Status");
            DropIndex("dbo.OutboundCalls", new[] { "DateTimeOfCall" });
            DropIndex("dbo.OutboundCalls", new[] { "StatusId" });
            DropIndex("dbo.OutboundCalls", new[] { "UserId" });
            DropIndex("dbo.Users", "FullName");
            DropIndex("dbo.Status", new[] { "Description" });
            DropIndex("dbo.InboundCalls", new[] { "DateTimeOfCall" });
            DropIndex("dbo.InboundCalls", new[] { "StatusId" });
            DropIndex("dbo.InboundCalls", new[] { "UserId" });
            DropTable("dbo.PriorityQueues");
            DropTable("dbo.OutboundCalls");
            DropTable("dbo.NormalQueues");
            DropTable("dbo.Users");
            DropTable("dbo.Status");
            DropTable("dbo.InboundCalls");
        }
    }
}
