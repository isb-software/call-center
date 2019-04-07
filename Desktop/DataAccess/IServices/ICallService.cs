using System.Collections.Generic;
using Entities.Models;

namespace DataAccess.IServices
{
    public interface ICallService
    {
        void Create(Call call);

        List<Call> GetAll();
    }
}
