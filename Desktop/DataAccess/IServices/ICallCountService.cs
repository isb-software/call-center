using System;

using Entities.Dtos;

namespace DataAccess.IServices
{
    public interface ICallCountService
    {
        CallDashboardDto GetDashboardCalls();

        void IncreaseCount(int statusId);
    }
}
