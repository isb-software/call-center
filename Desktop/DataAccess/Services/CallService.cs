using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using DataAccess.IServices;
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
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Calls.Add(call);

                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception exception)
                {
                    Log.Error($"Error on creating a new call for {call.PhoneNumber} - {call.Name} {call.Forename}", exception);
                    dbContextTransaction.Rollback();
                }
            }
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
                    IQueryable<Call> queryable = context.Calls.Include(x => x.Status).Include(x => x.User);

                    if (queryOptions.SearchTerm != null)
                    {
                        queryable = queryable
                            .Where(x => x.User.FirstName.ToLower().Contains(queryOptions.SearchTerm) ||
                                        x.User.LastName.ToLower().Contains(queryOptions.SearchTerm) ||
                                        x.Name.ToLower().Contains(queryOptions.SearchTerm) ||
                                        x.Forename.ToLower().Contains(queryOptions.SearchTerm) ||
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
                                     Age = x.Age,
                                     City = x.City,
                                     Education = x.Education,
                                     County = x.County,
                                     PersonName = x.Name + " " + x.Forename,
                                     StatusName = x.Status.Description,
                                     UserName = x.User.FirstName + " " + x.User.LastName
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

        public Call GetByPhoneNumber(string phoneNumber)
        {
            Call call = null;

            using (var context = new CallCenterDbContext())
            {
                call = context.Calls.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            }

            return call;
        }
    }
}
