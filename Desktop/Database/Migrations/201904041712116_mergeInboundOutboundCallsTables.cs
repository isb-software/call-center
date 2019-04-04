namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mergeInboundOutboundCallsTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.InboundCalls", newName: "Calls");
            DropIndex("dbo.OutboundCalls", new[] { "UserId" });
            DropIndex("dbo.OutboundCalls", new[] { "StatusId" });
            DropIndex("dbo.OutboundCalls", new[] { "DateTimeOfCall" });
            AddColumn("dbo.Calls", "CallType", c => c.Int(nullable: false));
            DropTable("dbo.OutboundCalls");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Calls", "CallType");
            CreateIndex("dbo.OutboundCalls", "DateTimeOfCall");
            CreateIndex("dbo.OutboundCalls", "StatusId");
            CreateIndex("dbo.OutboundCalls", "UserId");
            RenameTable(name: "dbo.Calls", newName: "InboundCalls");
        }
    }
}
