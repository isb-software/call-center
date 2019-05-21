using DataAccess.IServices;
using Database;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class InitialDataService : IInitialDataService
    {
        public InitialData GetByPhoneNumber(string phoneNumber)
        {
            InitialData initialData = null;

            using (var context = new CallCenterDbContext())
            {
                initialData = context.InitialDatas.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            }

            return initialData;
        }
    }
}
