namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeColumnName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Calls", "City", c => c.String());
            DropColumn("dbo.Calls", "Localitate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Calls", "Localitate", c => c.String());
            DropColumn("dbo.Calls", "City");
        }
    }
}
