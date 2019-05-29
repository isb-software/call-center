namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeNullableInitialData : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.InitialDatas", new[] { "AgeRangeId" });
            DropIndex("dbo.InitialDatas", new[] { "EducationTypeId" });
            DropIndex("dbo.InitialDatas", new[] { "EmployeeTypeId" });
            AlterColumn("dbo.InitialDatas", "AgeRangeId", c => c.Int());
            AlterColumn("dbo.InitialDatas", "EducationTypeId", c => c.Int());
            AlterColumn("dbo.InitialDatas", "EmployeeTypeId", c => c.Int());
            CreateIndex("dbo.InitialDatas", "AgeRangeId");
            CreateIndex("dbo.InitialDatas", "EducationTypeId");
            CreateIndex("dbo.InitialDatas", "EmployeeTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.InitialDatas", new[] { "EmployeeTypeId" });
            DropIndex("dbo.InitialDatas", new[] { "EducationTypeId" });
            DropIndex("dbo.InitialDatas", new[] { "AgeRangeId" });
            AlterColumn("dbo.InitialDatas", "EmployeeTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.InitialDatas", "EducationTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.InitialDatas", "AgeRangeId", c => c.Int(nullable: false));
            CreateIndex("dbo.InitialDatas", "EmployeeTypeId");
            CreateIndex("dbo.InitialDatas", "EducationTypeId");
            CreateIndex("dbo.InitialDatas", "AgeRangeId");
        }
    }
}
