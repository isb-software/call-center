using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Entities.Models;

namespace Database.Migrations
{
    public class Configuration : DbMigrationsConfiguration<CallCenterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            // Note: if we add new procedures or change a procedure we need to add another empty migration so that the seed method will run
            var migrator = new DbMigrator(this);
            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
        }

        protected override void Seed(CallCenterDbContext context)
        {
            SeedStatusTable(context);
            SeedProcedures(context);
            SeedUsersTable(context);
        }

        private void SeedProcedures(CallCenterDbContext context)
        {
            DeleteStoredProcedures(context);
            CreateStoredProcedures(context);
        }

        private void CreateStoredProcedures(CallCenterDbContext context)
        {
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Procedures\\Create"), "*.sql"))
            {
                try
                {
                    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Procedure already exists {file}");
                }
            }
        }

        private void DeleteStoredProcedures(CallCenterDbContext context)
        {
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Procedures\\Delete"), "*.sql"))
            {
                try
                {
                    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Procedure does not exist {file}");
                }
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

        private void SeedUsersTable(CallCenterDbContext context)
        {
            context.Users.AddOrUpdate(
                x => x.Id,
                new User
                {
                    Id = 3,
                    CreatedDate = DateTime.Now,
                    FirstName = "Test",
                    IsActive = true,
                    LastName = "User"
                });

            context.SaveChanges();
        }
    }
}
