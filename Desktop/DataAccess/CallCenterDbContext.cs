using System.Data.Entity;
using Entities.Models;

namespace DataAccess
{
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class CallCenterDbContext : DbContext
    {
        public CallCenterDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PriorityQueue> PriorityQueue { get; set; }

        public DbSet<NormalQueue> NormalQueue { get; set; }

        public DbSet<OutboundCall> OutboundCalls { get; set; }

        public DbSet<InboundCall> InboundCalls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
