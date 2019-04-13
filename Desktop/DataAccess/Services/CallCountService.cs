using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

using Common.Extensions;
using DataAccess.IServices;
using Database;
using Entities.Dtos;
using Entities.Models;

using RefactorThis.GraphDiff;

namespace DataAccess.Services
{
    public class CallCountService : ICallCountService
    {
        public CallDashboardDto GetDashboardCalls()
        {
            var callDashboardDto = new CallDashboardDto
                                                    {
                                                        DailyCalls = this.GetDailyCallCounts(),
                                                        WeeklyCalls = this.GetWeeklyCallCounts(),
                                                        MonthlyCalls = this.GetMonthlyCallCounts(),
                                                        YearlyCalls = this.GetYearlyCallCounts()
                                                    };


            return callDashboardDto;
        }

        public void IncreaseCount(DateTime date, int statusId)
        {
            using (var context = new CallCenterDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var countOfDate = context.CallCounts
                            .FirstOrDefault(x => DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(date) && x.StatusId == statusId);

                        if (countOfDate == null)
                        {
                            var newCallCount = new CallCount
                                                   {
                                                       Count = 1,
                                                       Date = date,
                                                       StatusId = statusId
                                                   };

                            context.CallCounts.Add(newCallCount);
                        }
                        else
                        {
                            countOfDate.Count = countOfDate.Count + 1;
                        }

                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        private List<CallCountDto> GetDailyCallCounts()
        {
            List<CallCountDto> dailyCallCounts;

            using (var context = new CallCenterDbContext())
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

            return dailyCallCounts;
        }

        private List<CallCountDto> GetWeeklyCallCounts()
        {
            List<CallCountDto> weeklyCallCounts = new List<CallCountDto>();

            using (var context = new CallCenterDbContext())
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

            return weeklyCallCounts;
        }

        private List<CallCountDto> GetMonthlyCallCounts()
        {
            List<CallCountDto> monthlyCallCounts;

            using (var context = new CallCenterDbContext())
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

            return monthlyCallCounts;
        }

        private List<CallCountDto> GetYearlyCallCounts()
        {
            List<CallCountDto> yearlyCallCounts;

            using (var context = new CallCenterDbContext())
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

            return yearlyCallCounts;
        }
    }
}
