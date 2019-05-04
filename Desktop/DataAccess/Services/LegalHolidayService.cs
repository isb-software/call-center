using DataAccess.IServices;
using Database;
using System;
using System.Linq;

namespace DataAccess.Services
{
    public class LegalHolidayService : ILegalHolidayService
    {
        public bool IsLegalHoliday()
        {
            bool isLegalHoliday;

            using (var context = new CallCenterDbContext())
            {
                var currentDate = DateTime.Now;

                isLegalHoliday = context.LegalHolidays.Any(x => x.Day == currentDate.Day && x.Month == currentDate.Month);
            }

            return isLegalHoliday;
        }
    }
}
