﻿using Entities.Models;

namespace DataAccess.IServices
{
    public interface IInitialDataService
    {
        InitialData GetByPhoneNumber(string phoneNumber);

        void Update(InitialData initialData);
    }
}
