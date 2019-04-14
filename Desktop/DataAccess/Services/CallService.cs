using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.IServices;
using Database;
using Entities.Dtos;
using Entities.Models;
using Entities.QueryOptions;

namespace DataAccess.Services
{
    public class CallService : ICallService
    {
        public void Create(Call call)
        {
            using (var context = new CallCenterDbContext())
            {
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
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        public CallDatasourceDto GetDatasource(TableQueryOptions queryOptions)
        {
            var datasource = new CallDatasourceDto();
            List<CallDto> calls;
            var totalRecords = 0;

            using (var context = new CallCenterDbContext())
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

            datasource.Calls = calls;
            datasource.TotalRecords = totalRecords;
            return datasource;
        }
    }
}
