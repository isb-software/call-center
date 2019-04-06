using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PriorityQueue
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
