using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class QueuePhoneNumber
    {
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public int CallAtempts { get; set; }
    }
}
