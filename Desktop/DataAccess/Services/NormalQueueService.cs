using Common.Extensions;
using DataAccess.IServices;
using DataAccess.PollyPolicies;
using Database;
using Entities.Models;
using log4net;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace DataAccess.Services
{
    public class NormalQueueService : INormalQueueService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int callAtemptsConfiguration;
        private int callAtemptDelayConfiguration;

        public NormalQueueService()
        {
            this.callAtemptsConfiguration = Convert.ToInt32(ConfigurationManager.AppSettings["SipCallAtempts"]);
            this.callAtemptDelayConfiguration = Convert.ToInt32(ConfigurationManager.AppSettings["SipCallAtemptDelayInDays"]);
        }

        public void Create(QueuePhoneNumber queuePhoneNumber)
        {
            var nextCallAtempt = queuePhoneNumber.CallAtempts + 1;
            if(nextCallAtempt > callAtemptsConfiguration)
            {
                return;
            }

            using (var context = new CallCenterDbContext())
            {
                DbContextTransaction dbContextTransaction = null;
                try
                {
                    using (dbContextTransaction = context.Database.BeginTransaction())
                    {
                        PollyPolicy.WaitAndRetryThreeTimes.Execute(() => TryCreate(queuePhoneNumber, context, dbContextTransaction));
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"Error on creating a new normal queue for {queuePhoneNumber?.PhoneNumber}", exception);

                    dbContextTransaction?.Rollback();
                }
            }
        }

        public QueuePhoneNumber GetNextNumber()
        {
            try
            {
                QueuePhoneNumber queuePhoneNumber;

                using (var context = new CallCenterDbContext())
                {
                    queuePhoneNumber = context.Database.SqlQuery<QueuePhoneNumber>("GetNextNormalPhoneNumber").FirstOrDefault();
                }

                return queuePhoneNumber;
            }
            catch (Exception exception)
            {
                Log.Error("Error on getting the next number", exception);
                throw;
            }
        }

        private void TryCreate(QueuePhoneNumber call, CallCenterDbContext context, DbContextTransaction dbContextTransaction)
        {
            NormalQueue normalQueue = new NormalQueue
            {
                PhoneNumber = call.PhoneNumber,
                CallAtempts = call.CallAtempts + 1,
                NextTimeCall = DateTime.Now.AddBusinessDays(callAtemptDelayConfiguration)
            };

            context.NormalQueue.Add(normalQueue);
            context.SaveChanges();
            dbContextTransaction.Commit();
        }
    }
}
