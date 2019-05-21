using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class QueuePhoneNumber
    {
        [MaxLength(30)]
        [Required]
        public string PhoneNumber { get; set; }

        public int CallAtempts { get; set; }
    }
}
