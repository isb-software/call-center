using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Status
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Description { get; set; }
    }
}
