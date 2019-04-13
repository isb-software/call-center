namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnsToCalls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calls", "PhoneNumber", c => c.String());
            AddColumn("dbo.Calls", "Name", c => c.String());
            AddColumn("dbo.Calls", "Forename", c => c.String());
            AddColumn("dbo.Calls", "County", c => c.String());
            AddColumn("dbo.Calls", "Localitate", c => c.String());
            AddColumn("dbo.Calls", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Calls", "Education", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Calls", "Education");
            DropColumn("dbo.Calls", "Age");
            DropColumn("dbo.Calls", "Localitate");
            DropColumn("dbo.Calls", "County");
            DropColumn("dbo.Calls", "Forename");
            DropColumn("dbo.Calls", "Name");
            DropColumn("dbo.Calls", "PhoneNumber");
        }
    }
}
