using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccess.IServices;
using Database;
using Entities.Models;
using log4net;
using RefactorThis.GraphDiff;

namespace DataAccess.Services
{
    public class UserService : IUserService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<User> GetAll()
        {
            List<User> users;

            using (var context = new CallCenterDbContext())
            {
                users = context.Users.ToList();

                if (!users.Any())
                {
                    Log.Warn("No users found at all");
                }
            }

            return users;
        }

        public User GetById(int userId)
        {
            User user;

            using (var context = new CallCenterDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.Id == userId);

                if (user == null)
                {
                    string message = $"No users found by id {userId} when getting by id";
                    Log.Error(message);
                    throw new Exception(message);
                }
            }

            return user;
        }

        public void Create(User user)
        {
            using (var context = new CallCenterDbContext())
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Users.Add(user);

                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error creating the user {user.FirstName} {user.LastName}", exception);
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void Update(User user)
        {
            using (var context = new CallCenterDbContext())
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.UpdateGraph(user);

                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error updating the user with id {user.Id}", exception);
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void Delete(int userId)
        {
            using (var context = new CallCenterDbContext())
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(x => x.Id == userId);

                    if (user == null)
                    {
                        string message = $"No users found by id {userId} when deleting by id";
                        Log.Error(message);
                        throw new Exception(message);
                    }

                    context.Users.Remove(user);
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error deleting the user with id {userId}", exception);
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
