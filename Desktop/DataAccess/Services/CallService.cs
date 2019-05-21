using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using DataAccess.IServices;
using DataAccess.PollyPolicies;
using Database;
using Entities.Dtos;
using Entities.Models;
using Entities.QueryOptions;
using log4net;

namespace DataAccess.Services
{
    public class CallService : ICallService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Create(Call call)
        {
            using (var context = new CallCenterDbContext())
            {
                DbContextTransaction dbContextTransaction = null;
                try
                {
                    using (dbContextTransaction = context.Database.BeginTransaction())
                    {
                        PollyPolicy.WaitAndRetryThreeTimes.Execute(() => TryCreate(call, context, dbContextTransaction));
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"Error on creating a new call for {call?.PhoneNumber}", exception);

                    dbContextTransaction?.Rollback();
                }
            }
        }

        private static void TryCreate(Call call, CallCenterDbContext context, DbContextTransaction dbContextTransaction)
        {
            context.Calls.Add(call);

            context.SaveChanges();
            dbContextTransaction.Commit();
        }


        public CallDatasourceDto GetDatasource(TableQueryOptions queryOptions)
        {
            var datasource = new CallDatasourceDto();
            List<CallDto> calls = new List<CallDto>();
            var totalRecords = 0;

            using (var context = new CallCenterDbContext())
            {
                try
                {
                    IQueryable<Call> queryable = context.Calls.Include(x => x.Status).Include(x => x.User).Include(x => x.InitialData);

                    if (queryOptions.SearchTerm != null)
                    {
                        queryable = queryable
                            .Where(x => x.User.FirstName.ToLower().Contains(queryOptions.SearchTerm) ||
                                        x.User.LastName.ToLower().Contains(queryOptions.SearchTerm) ||
                                        x.InitialData.Name.ToLower().Contains(queryOptions.SearchTerm) ||
                                        x.InitialData.Forename.ToLower().Contains(queryOptions.SearchTerm) ||
                                        x.PhoneNumber.ToLower().Contains(queryOptions.SearchTerm));
                    }

                    totalRecords = queryable.Count();

                    queryable = queryOptions.SortType == "asc" ?
                                    queryable.OrderBy(x => x.DateTimeOfCall) :
                                    queryable.OrderByDescending(x => x.DateTimeOfCall);

                    queryable = queryable.Skip(queryOptions.Offset).Take(queryOptions.Limit);

                    calls = queryable.Select(
                        x => new CallDto
                                 {
                                     DateTimeOfCall = x.DateTimeOfCall,
                                     PhoneNumber = x.PhoneNumber,
                                     Notes = x.Notes,
                                     Age = x.InitialData.AgeRange.Range,
                                     City = x.InitialData.City,
                                     Education = x.InitialData.EducationType.Name,
                                     County = x.InitialData.County,
                                     PersonName = x.InitialData.Name + " " + x.InitialData.Forename,
                                     StatusName = x.Status.Description,
                                     UserName = x.User.FirstName + " " + x.User.LastName,
                                     Duration = x.Duration
                                 }).ToList();

                }
                catch (Exception exception)
                {
                    Log.Error($"Error on getting teh call datasource", exception);
                }
            }

            datasource.Calls = calls;
            datasource.TotalRecords = totalRecords;
            return datasource;
        }
    }
}
