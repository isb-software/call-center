using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("FullName", 1, IsUnique = true)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("FullName", 2, IsUnique = true)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
