using System;
using System.Linq;
using System.Reflection;
using DataAccess.IServices;
using Database;
using log4net;

namespace DataAccess.Services
{
    public class PriorityQueueService : IPriorityQueueService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string GetNextNumber()
        {
            try
            {
                string number;

                using (var context = new CallCenterDbContext())
                {
                    number = context.Database.SqlQuery<string>("GetNextPriorityPhoneNumber").First();
                }

                return number;
            }
            catch (Exception exception)
            {
                Log.Error("Error on getting the next number", exception);
                throw;
            }
        }
    }
}
