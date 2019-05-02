using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DataAccess.IServices;
using Database;
using Entities.Models;

using log4net;

namespace DataAccess.Services
{
    public class StatusService : IStatusService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<Status> GetAll()
        {
            List<Status> statuses;

            using (var context = new CallCenterDbContext())
            {
                statuses = context.Statuses.ToList();
                if (!statuses.Any())
                {
                    Log.Warn("No statuses found at all");
                }
            }

            return statuses;
        }
    }
}
