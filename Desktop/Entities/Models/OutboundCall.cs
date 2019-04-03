using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class OutboundCall
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }

        [Index]
        public DateTime DateTimeOfCall { get; set; }

        public int Duration { get; set; }

        public string RecordingPath { get; set; }
    }
}
