namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    using Entities.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CallCenterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CallCenterDbContext context)
        {
            SeedStatusTable(context);
        }

        private void SeedStatusTable(CallCenterDbContext context)
        {
            context.Statuses.AddOrUpdate(
                x => x.Id,
                new Status
                    {
                        Id = 1,
                        Description = "Ocupat"
                    });

            context.Statuses.AddOrUpdate(
                x => x.Id,
                new Status
                    {
                        Id = 2,
                        Description = "Numar Inexistent"
                    });

            context.Statuses.AddOrUpdate(
                x => x.Id,
                new Status
                    {
                        Id = 3,
                        Description = "Neinteresat"
                    });

            context.Statuses.AddOrUpdate(
                x => x.Id,
                new Status
                    {
                        Id = 4,
                        Description = "Succes"
                    });

            context.Statuses.AddOrUpdate(
                x => x.Id,
                new Status
                    {
                        Id = 5,
                        Description = "Nu este in grupa de varsta"
                    });

            context.SaveChanges();
        }
    }
}
