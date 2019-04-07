using System.Collections.Generic;
using System.Linq;
using DataAccess.IServices;
using Database;
using Entities.Models;

namespace DataAccess.Services
{
    public class StatusService : IStatusService
    {
        public List<Status> GetAll()
        {
            List<Status> statuses;

            using (var context = new CallCenterDbContext())
            {
                statuses = context.Statuses.ToList();
            }

            return statuses;
        }
    }
}
