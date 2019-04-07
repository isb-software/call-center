using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class CallCount
    {
        public int Id { get; set; }

        public int Count { get; set; }

        [Index]
        public DateTime Date { get; set; }

        public Status Status { get; set; }

        public int StatusId { get; set; }

    }
}
