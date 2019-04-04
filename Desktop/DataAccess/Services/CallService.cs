using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.IServices;
using Database;
using Entities.Models;

namespace DataAccess.Services
{
    public class CallService : ICallService
    {
        public void Create(Call call)
        {
            using (var context = new CallCenterDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Calls.Add(call);

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

        public List<Call> GetAll()
        {
            List<Call> calls;

            using (var context = new CallCenterDbContext())
            {
                calls = context.Calls.ToList();
            }

            return calls;
        }
    }
}
