using System;

namespace Entities.Models
{
    public class PriorityQueue : QueuePhoneNumber
    {
        public int Id { get; set; }

        public DateTime NextTimeCall { get; set; }
    }
}
