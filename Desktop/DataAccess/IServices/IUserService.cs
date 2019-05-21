using System.Collections.Generic;
using Entities.Models;

namespace DataAccess.IServices
{
    public interface IUserService
    {
        List<User> GetAll(bool isActive);

        User GetById(int userId);

        void Create(User user);

        void Update(User user);

        void Delete(int userId);
    }
}
