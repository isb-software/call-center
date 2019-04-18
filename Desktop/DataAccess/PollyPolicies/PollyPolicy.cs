using System;
using System.Reflection;
using log4net;
using Polly;

namespace DataAccess.PollyPolicies
{
    public static class PollyPolicy
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly Policy WaitAndRetryThreeTimes = Policy.Handle<Exception>().WaitAndRetry(
            new[]
                {
                    TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4)
                },
            (exception, timeSpan, retryCount, context) =>
                {
                    Log.Error($"Error on WaitAndRetry for {retryCount} time", exception);
                });
    }
}
