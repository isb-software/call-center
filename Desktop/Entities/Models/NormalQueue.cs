using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class NormalQueue
    {
        public int Id { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
