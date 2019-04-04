using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.IServices;
using Database;
using Entities.Models;
using RefactorThis.GraphDiff;

namespace DataAccess.Services
{
    public class UserService : IUserService
    {
        public List<User> GetAll()
        {
            List<User> users;

            using (var context = new CallCenterDbContext())
            {
                users = context.Users.ToList();
            }

            return users;
        }

        public User GetById(int userId)
        {
            User user;

            using (var context = new CallCenterDbContext())
            {
                user = context.Users.FirstOrDefault(x => x.Id == userId);
            }

            return user;
        }

        public void Create(User user)
        {
            using (var context = new CallCenterDbContext())
            {
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
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        public void Update(User user)
        {
            using (var context = new CallCenterDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.UpdateGraph(user);

                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        public void Delete(int userId)
        {
            using (var context = new CallCenterDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var user = context.Users.FirstOrDefault(x => x.Id == userId);

                        if (user == null)
                        {
                            throw new Exception("User not found");
                        }

                        context.Users.Remove(user);
                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }
    }
}
