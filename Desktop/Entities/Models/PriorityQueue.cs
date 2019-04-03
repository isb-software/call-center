using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class PriorityQueue
    {
        [Key]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
