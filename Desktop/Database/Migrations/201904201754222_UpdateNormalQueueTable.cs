namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNormalQueueTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.NormalQueues");
            AddColumn("dbo.NormalQueues", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(maxLength: 15));
            AddPrimaryKey("dbo.NormalQueues", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.NormalQueues");
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.NormalQueues", "Id");
            AddPrimaryKey("dbo.NormalQueues", "PhoneNumber");
        }
    }
}
