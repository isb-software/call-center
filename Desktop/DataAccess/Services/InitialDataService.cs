using DataAccess.IServices;
using Database;
using Entities.Models;
using RefactorThis.GraphDiff;
using System;
using System.Linq;
using System.Reflection;
using log4net;

namespace DataAccess.Services
{
    public class InitialDataService : IInitialDataService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public InitialData GetByPhoneNumber(string phoneNumber)
        {
            InitialData initialData = null;

            using (var context = new CallCenterDbContext())
            {
                initialData = context.InitialDatas.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            }

            return initialData;
        }

        public void Update(InitialData initialData)
        {
            using (var context = new CallCenterDbContext())
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.UpdateGraph(initialData);

                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error updating the intial data with id {initialData.Id}", exception);
                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
