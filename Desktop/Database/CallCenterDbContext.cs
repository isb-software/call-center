using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Database.Migrations;
using Entities.Models;

namespace Database
{
    public class CallCenterDbContext : DbContext
    {
        public CallCenterDbContext() : base("DefaultConnection")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<CallCenterDbContext, Configuration>());
        }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PriorityQueue> PriorityQueue { get; set; }

        public DbSet<NormalQueue> NormalQueue { get; set; }

        public DbSet<Call> Calls { get; set; }

        public DbSet<CallCount> CallCounts { get; set; }

        public DbSet<LegalHoliday> LegalHolidays { get; set; }

        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        public DbSet<AgeRange> AgeRanges { get; set; }

        public DbSet<EducationType> EducationTypes { get; set; }

        public DbSet<InitialData> InitialDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
