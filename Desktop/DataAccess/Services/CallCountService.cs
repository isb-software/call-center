﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using DataAccess.IServices;
using DataAccess.PollyPolicies;
using Database;
using Entities.Dtos;
using Entities.Models;
using log4net;

namespace DataAccess.Services
{
    public class CallCountService : ICallCountService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CallDashboardDto GetDashboardCalls()
        {
            var callDashboardDto = new CallDashboardDto
                                                    {
                                                        DailyCalls = GetDailyCallCounts(),
                                                        WeeklyCalls = GetWeeklyCallCounts(),
                                                        MonthlyCalls = GetMonthlyCallCounts(),
                                                        YearlyCalls = GetYearlyCallCounts()
                                                    };

            return callDashboardDto;
        }

        public void IncreaseCount(int statusId)
        {
            using (var context = new CallCenterDbContext())
            {
                DbContextTransaction dbContextTransaction = null;
                try
                {
                    using (dbContextTransaction = context.Database.BeginTransaction())
                    {
                        PollyPolicy.WaitAndRetryThreeTimes.Execute(() => TryIncreaseCount(statusId, context, dbContextTransaction));
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"Error increasing the count at date {DateTime.Now} for status id {statusId}", exception);

                    dbContextTransaction?.Rollback();
                }
            }
        }

        private void TryIncreaseCount(int statusId, CallCenterDbContext context, DbContextTransaction dbContextTransaction)
        {
            var currentDate = DateTime.Now;
            var countOfDate = context.CallCounts.FirstOrDefault(x => DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(currentDate) && x.StatusId == statusId);

            if (countOfDate == null)
            {
                var newCallCount = new CallCount
                                       {
                                           Count = 1,
                                           Date = currentDate,
                                           StatusId = statusId
                };

                context.CallCounts.Add(newCallCount);
            }
            else
            {
                countOfDate.Count = countOfDate.Count + 1;
                countOfDate.StatusId = statusId;
            }

            context.SaveChanges();
            dbContextTransaction.Commit();
        }

        private List<CallCountDto> GetDailyCallCounts()
        {
            List<CallCountDto> dailyCallCounts = new List<CallCountDto>();

            using (var context = new CallCenterDbContext())
            {
                try
                {
                    dailyCallCounts = context.CallCounts
                        .Where(x => DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now))
                        .Select(x => new CallCountDto
                                         {
                                             Count = x.Count,
                                             StatusId = x.StatusId
                                         })
                        .ToList();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error getting the daily counts on {DateTime.Now}", exception);
                }

            }

            return dailyCallCounts;
        }

        private List<CallCountDto> GetWeeklyCallCounts()
        {
            List<CallCountDto> weeklyCallCounts = new List<CallCountDto>();

            using (var context = new CallCenterDbContext())
            {
                try
                {
                    DateTime currentDay = DateTime.Today;
                    var weekStartDate = currentDay.AddDays(-(int)currentDay.DayOfWeek);
                    var weekEndDate = weekStartDate.AddDays(7).AddSeconds(-1);

                    weeklyCallCounts = context.CallCounts.Where(x => x.Date >= weekStartDate && x.Date <= weekEndDate).GroupBy(x => x.StatusId).Select(
                        x => new CallCountDto
                                 {
                                     StatusId = x.Key,
                                     Count = x.Sum(y => y.Count)
                                 }).ToList();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error getting the weekly counts on {DateTime.Today}", exception);
                }
            }

            return weeklyCallCounts;
        }

        private List<CallCountDto> GetMonthlyCallCounts()
        {
            List<CallCountDto> monthlyCallCounts = new List<CallCountDto>();

            using (var context = new CallCenterDbContext())
            {
                try
                {
                    DateTime currentDay = DateTime.Today;

                    var monthStartDate = currentDay.AddDays(1 - currentDay.Day);
                    var monthEndDate = monthStartDate.AddMonths(1).AddSeconds(-1);

                    monthlyCallCounts = context.CallCounts.Where(x => x.Date >= monthStartDate && x.Date <= monthEndDate).GroupBy(x => x.StatusId).Select(
                        x => new CallCountDto
                                 {
                                     StatusId = x.Key,
                                     Count = x.Sum(y => y.Count)
                                 }).ToList();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error getting the monthly counts on {DateTime.Today}", exception);
                }
            }

            return monthlyCallCounts;
        }

        private List<CallCountDto> GetYearlyCallCounts()
        {
            List<CallCountDto> yearlyCallCounts = new List<CallCountDto>();

            using (var context = new CallCenterDbContext())
            {
                try
                {
                    int year = DateTime.Now.Year;
                    var firstDayOfYear = new DateTime(year, 1, 1);
                    var lastDayOfYear = new DateTime(year, 12, 31);

                    yearlyCallCounts = context.CallCounts.Where(x => x.Date >= firstDayOfYear && x.Date <= lastDayOfYear).GroupBy(x => x.StatusId).Select(
                        x => new CallCountDto
                                 {
                                     StatusId = x.Key,
                                     Count = x.Sum(y => y.Count)
                                 }).ToList();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error getting the yearly counts on {DateTime.Today}", exception);
                }
            }

            return yearlyCallCounts;
        }
    }
}
