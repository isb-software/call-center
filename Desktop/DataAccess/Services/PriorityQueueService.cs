using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Common.Extensions;
using DataAccess.IServices;
using DataAccess.PollyPolicies;
using Database;
using Entities.Models;
using log4net;

namespace DataAccess.Services
{
    public class PriorityQueueService : IPriorityQueueService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int callAtemptsConfiguration;
        private int callAtemptDelayConfiguration;

        public PriorityQueueService()
        {
            this.callAtemptsConfiguration = Convert.ToInt32(ConfigurationManager.AppSettings["SipCallAtempts"]);
            this.callAtemptDelayConfiguration = Convert.ToInt32(ConfigurationManager.AppSettings["SipCallAtemptDelayInDays"]);
        }

        public void Create(QueuePhoneNumber queuePhoneNumber)
        {
            var nextCallAtempt = queuePhoneNumber.CallAtempts + 1;
            if (nextCallAtempt > callAtemptsConfiguration)
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
                    queuePhoneNumber = context.Database.SqlQuery<QueuePhoneNumber>("GetNextPriorityPhoneNumber").FirstOrDefault();
                }

                return queuePhoneNumber;
            }
            catch (Exception exception)
            {
                Log.Error("Error on getting the next number", exception);
                //throw;
            }
            return null;
        }

        private void TryCreate(QueuePhoneNumber call, CallCenterDbContext context, DbContextTransaction dbContextTransaction)
        {
            PriorityQueue normalQueue = new PriorityQueue
            {
                PhoneNumber = call.PhoneNumber,
                CallAtempts = call.CallAtempts + 1,
                NextTimeCall = DateTime.Now.AddBusinessDays(callAtemptDelayConfiguration)
            };

            context.PriorityQueue.Add(normalQueue);
            context.SaveChanges();
            dbContextTransaction.Commit();
        }
    }
}
