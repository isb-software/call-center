namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Calls", "PhoneNumber", c => c.String(maxLength: 30));
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.PriorityQueues", "PhoneNumber", c => c.String(maxLength: 30, unicode: false));
            CreateIndex("dbo.NormalQueues", "PhoneNumber", unique: true);
            CreateIndex("dbo.PriorityQueues", "PhoneNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PriorityQueues", new[] { "PhoneNumber" });
            DropIndex("dbo.NormalQueues", new[] { "PhoneNumber" });
            AlterColumn("dbo.PriorityQueues", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Calls", "PhoneNumber", c => c.String());
        }
    }
}
