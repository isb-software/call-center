﻿using Entities.Enums;

namespace Entities.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Forename { get; set; }

        public string County { get; set; }

        public string Localitate { get; set; }

        public int Age { get; set; }

        public string Education { get; set; }
    }
}