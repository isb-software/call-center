using System;

namespace Entities.Models
{
    public class NormalQueue : QueuePhoneNumber
    {
        public int Id { get; set; }

        public DateTime NextTimeCall { get; set; }
    }
}
