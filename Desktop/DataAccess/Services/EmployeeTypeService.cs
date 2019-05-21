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
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<EmployeeType> GetAll()
        {
            List<EmployeeType> employeeTypes;

            using (var context = new CallCenterDbContext())
            {
                employeeTypes = context.EmployeeTypes.ToList();
                if (!employeeTypes.Any())
                {
                    Log.Warn("No employee types found at all");
                }
            }

            return employeeTypes;
        }
    }
}
