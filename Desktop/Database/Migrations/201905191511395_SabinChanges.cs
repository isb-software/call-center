namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SabinChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeRanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Range = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InitialDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Name = c.String(),
                        Forename = c.String(),
                        County = c.String(),
                        City = c.String(),
                        AgeRangeId = c.Int(nullable: false),
                        EducationTypeId = c.Int(nullable: false),
                        EmployeeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgeRanges", t => t.AgeRangeId)
                .ForeignKey("dbo.EducationTypes", t => t.EducationTypeId)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeTypeId)
                .Index(t => t.AgeRangeId)
                .Index(t => t.EducationTypeId)
                .Index(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.EducationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Calls", "InitialDataId", c => c.Int(nullable: false));
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.PriorityQueues", "PhoneNumber", c => c.String(nullable: false));
            CreateIndex("dbo.Calls", "InitialDataId");
            AddForeignKey("dbo.Calls", "InitialDataId", "dbo.InitialDatas", "Id");
            DropColumn("dbo.Calls", "Name");
            DropColumn("dbo.Calls", "Forename");
            DropColumn("dbo.Calls", "County");
            DropColumn("dbo.Calls", "City");
            DropColumn("dbo.Calls", "Age");
            DropColumn("dbo.Calls", "Education");
            DropColumn("dbo.NormalQueues", "NextTimeCall");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NormalQueues", "NextTimeCall", c => c.DateTime(nullable: false));
            AddColumn("dbo.Calls", "Education", c => c.String());
            AddColumn("dbo.Calls", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Calls", "City", c => c.String());
            AddColumn("dbo.Calls", "County", c => c.String());
            AddColumn("dbo.Calls", "Forename", c => c.String());
            AddColumn("dbo.Calls", "Name", c => c.String());
            DropForeignKey("dbo.Calls", "InitialDataId", "dbo.InitialDatas");
            DropForeignKey("dbo.InitialDatas", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropForeignKey("dbo.InitialDatas", "EducationTypeId", "dbo.EducationTypes");
            DropForeignKey("dbo.InitialDatas", "AgeRangeId", "dbo.AgeRanges");
            DropIndex("dbo.InitialDatas", new[] { "EmployeeTypeId" });
            DropIndex("dbo.InitialDatas", new[] { "EducationTypeId" });
            DropIndex("dbo.InitialDatas", new[] { "AgeRangeId" });
            DropIndex("dbo.Calls", new[] { "InitialDataId" });
            AlterColumn("dbo.PriorityQueues", "PhoneNumber", c => c.String(maxLength: 15));
            AlterColumn("dbo.NormalQueues", "PhoneNumber", c => c.String(maxLength: 15));
            DropColumn("dbo.Calls", "InitialDataId");
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.EducationTypes");
            DropTable("dbo.InitialDatas");
            DropTable("dbo.AgeRanges");
        }
    }
}
