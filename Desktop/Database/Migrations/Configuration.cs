using System;
using System.Data.Entity.Migrations;
using System.IO;
using Entities.Models;

namespace Database.Migrations
{
    public class Configuration : DbMigrationsConfiguration<CallCenterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CallCenterDbContext context)
        {
            SeedStatusTable(context);
            SeedProcedures(context);
        }

        private void SeedProcedures(CallCenterDbContext context)
        {
            DeleteStoredProcedures(context);
            CreateStoredProcedures(context);
        }

        private void CreateStoredProcedures(CallCenterDbContext context)
        {
            try
            {
                foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Procedures\\Create"), "*.sql"))
                {
                    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Procedure already exists");
            }
        }

        private void DeleteStoredProcedures(CallCenterDbContext context)
        {
            try
            {
                foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Procedures\\Delete"), "*.sql"))
                {
                    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Procedure does not exist");
            }
            
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
                        Description = "Nu raspunde"
                    });

            context.Statuses.AddOrUpdate(
                x => x.Id,
                new Status
                    {
                        Id = 6,
                        Description = "Fax"
                    });

            context.Statuses.AddOrUpdate(
                x => x.Id,
                new Status
                    {
                        Id = 7,
                        Description = "Casuta"
                    });

            context.SaveChanges();
        }
    }
}
