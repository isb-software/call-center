using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Entities.Models;

namespace Database
{

    public class CallCenterDbContext : DbContext
    {
        public CallCenterDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PriorityQueue> PriorityQueue { get; set; }

        public DbSet<NormalQueue> NormalQueue { get; set; }

        public DbSet<Call> Calls { get; set; }

        public DbSet<CallCount> CallCounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
