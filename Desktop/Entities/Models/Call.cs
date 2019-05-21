using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Models
{
    public class Call
    {
        public int Id { get; set; }

        [Index]
        public int UserId { get; set; }

        public User User { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }

        [Index]
        public DateTime DateTimeOfCall { get; set; }

        public int Duration { get; set; }

        public string RecordingPath { get; set; }

        public CallType CallType { get; set; }

        public string Notes { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        public InitialData InitialData { get; set; }

        public int InitialDataId { get; set; }
    }
}
