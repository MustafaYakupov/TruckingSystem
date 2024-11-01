using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TruckingSystem.Common.ValidationConstants.DriverManagerConstants;

namespace TruckingSystem.Data.Models
{
    public class DriverManager
    {
        public DriverManager()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Driver manager first name")]
        [MaxLength(DriverManagerFirstAndLastNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Comment("Driver manager last name")]
        [MaxLength(DriverManagerFirstAndLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public ICollection<Driver> Drivers { get; set; } = new HashSet<Driver>();

        public ICollection<BrokerCompany> BrokerCompanies { get; set; } = new HashSet<BrokerCompany>();

        public ICollection<Dispatch> Dispatches { get; set; } = new HashSet<Dispatch>();

        public bool IsDeleted { get; set; }
    }
}
