using System;
using System.Linq;

using DataAccess.IServices;
using Database;

using Entities.Models;

namespace DataAccess.Services
{
    public class PriorityQueueService : IPriorityQueueService
    {
        public string GetNextNumber()
        {
            string number;

            using (var context = new CallCenterDbContext())
            {
                 number = context.Database.SqlQuery<string>("GetNextPriorityPhoneNumber").First();
            }

            return number;
        }
    }
}
