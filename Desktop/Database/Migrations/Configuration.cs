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
            SeedLegalHolidaysTable(context);
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

        private void SeedLegalHolidaysTable(CallCenterDbContext context)
        {
            // Anul Nou 1 ianuarie	
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 1,
                    Day = 1,
                    Month = 1,
                });

            // A doua zi dupa Anul Nou 2 ianuarie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 2,
                    Day = 2,
                    Month = 1,
                });

            // Ziua Unirii Principatelor Romane 24 ianuarie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 3,
                    Day = 24,
                    Month = 1,
                });

            // Vinerea mare 26 aprilie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 4,
                    Day = 26,
                    Month = 4,
                });

            // Paste Ortodox 28 aprilie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 5,
                    Day = 28,
                    Month = 4,
                });

            // A doua zi de Paste Ortodox 29 aprilie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 6,
                    Day = 29,
                    Month = 4,
                });

            // Ziua Muncii 1 mai
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 7,
                    Day = 1,
                    Month = 5,
                });

            // Ziua Copilului 1 iunie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 8,
                    Day = 1,
                    Month = 6,
                });

            // Rusalii 16 iunie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 9,
                    Day = 16,
                    Month = 6,
                });

            // A doua zi de Rusalii 17 iunie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 10,
                    Day = 17,
                    Month = 6,
                });

            // Adormirea Maicii Domnului 15 august
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 11,
                    Day = 15,
                    Month = 8,
                });

            // Sfantul Andrei 30 noiembrie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 12,
                    Day = 30,
                    Month = 11,
                });

            // Ziua Nationala a Romaniei 1 decembrie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 13,
                    Day = 1,
                    Month = 12,
                });

            // Craciunul 25 decembrie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 14,
                    Day = 25,
                    Month = 12,
                });

            // A doua zi de Craciunul 26 decembrie
            context.LegalHolidays.AddOrUpdate(
                x => x.Id,
                new LegalHoliday
                {
                    Id = 15,
                    Day = 26,
                    Month = 12,
                });

            context.SaveChanges();
        }
    }
}
