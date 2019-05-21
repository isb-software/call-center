namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIndexPhoneNumber : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.NormalQueues", new[] { "PhoneNumber" });
            DropIndex("dbo.PriorityQueues", new[] { "PhoneNumber" });
            AlterColumn("dbo.InitialDatas", "PhoneNumber", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.PriorityQueues", "PhoneNumber", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.InitialDatas", "PhoneNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.InitialDatas", new[] { "PhoneNumber" });
            AlterColumn("dbo.PriorityQueues", "PhoneNumber", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.InitialDatas", "PhoneNumber", c => c.String());
            CreateIndex("dbo.PriorityQueues", "PhoneNumber", unique: true);
            CreateIndex("dbo.NormalQueues", "PhoneNumber", unique: true);
        }
    }
}
