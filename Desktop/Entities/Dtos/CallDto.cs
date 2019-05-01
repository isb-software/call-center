using System;

namespace Entities.Dtos
{
    public class CallDto
    {
        public string UserName { get; set; }

        public string PersonName { get; set; }

        public string PhoneNumber { get; set; }

        public string Notes { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Education { get; set; }

        public int Age { get; set; }

        public DateTime DateTimeOfCall { get; set; }

        public string StatusName { get; set; }

        public int Duration { get; set; }
    }
}
