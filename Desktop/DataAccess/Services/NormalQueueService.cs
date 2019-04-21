using Database;
using log4net;
using System;
using System.Linq;
using System.Reflection;

namespace DataAccess.Services
{
    public class NormalQueueService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string GetNextNumber()
        {
            try
            {
                string number;

                using (var context = new CallCenterDbContext())
                {
                    number = context.Database.SqlQuery<string>("GetNextNormalPhoneNumber").FirstOrDefault();
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
