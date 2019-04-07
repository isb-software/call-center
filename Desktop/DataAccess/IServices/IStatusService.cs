using System.Collections.Generic;
using Entities.Models;

namespace DataAccess.IServices
{

    public interface IStatusService
    {
        List<Status> GetAll();
    }
}
