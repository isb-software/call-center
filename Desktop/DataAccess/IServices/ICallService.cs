using Entities.Dtos;
using Entities.Models;
using Entities.QueryOptions;

namespace DataAccess.IServices
{
    public interface ICallService
    {
        void Create(Call call);

        CallDatasourceDto GetDatasource(TableQueryOptions queryOptions);
    }
}
