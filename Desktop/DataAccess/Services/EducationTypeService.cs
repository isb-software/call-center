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
    public class EducationTypeService : IEducationTypeService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<EducationType> GetAll()
        {
            List<EducationType> educationTypes;

            using (var context = new CallCenterDbContext())
            {
                educationTypes = context.EducationTypes.ToList();
                if (!educationTypes.Any())
                {
                    Log.Warn("No education types found found at all");
                }
            }

            return educationTypes;
        }
    }
}
