namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCallAtemptsAndNextTimeCall : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NormalQueues", "CallAtempts", c => c.Int(nullable: false));
            AddColumn("dbo.NormalQueues", "NextTimeCall", c => c.DateTime(nullable: false));
            AddColumn("dbo.PriorityQueues", "CallAtempts", c => c.Int(nullable: false));
            AddColumn("dbo.PriorityQueues", "NextTimeCall", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriorityQueues", "NextTimeCall");
            DropColumn("dbo.PriorityQueues", "CallAtempts");
            DropColumn("dbo.NormalQueues", "NextTimeCall");
            DropColumn("dbo.NormalQueues", "CallAtempts");
        }
    }
}
