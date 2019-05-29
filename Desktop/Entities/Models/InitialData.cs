using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class InitialData
    {
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Forename { get; set; }

        public string County { get; set; }

        public string City { get; set; }

        public AgeRange AgeRange { get; set; }

        public int? AgeRangeId { get; set; }

        public EducationType EducationType { get; set; }

        public int? EducationTypeId { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public int? EmployeeTypeId { get; set; }
    }
}
