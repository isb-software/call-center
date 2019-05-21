using DataAccess.IServices;
using Database;
using Entities.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class AgeRangeService : IAgeRangeService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<AgeRange> GetAll()
        {
            List<AgeRange> ageRanges;

            using (var context = new CallCenterDbContext())
            {
                ageRanges = context.AgeRanges.ToList();
                if (!ageRanges.Any())
                {
                    Log.Warn("No age ranges found at all");
                }
            }

            return ageRanges;
        }
    }
}
