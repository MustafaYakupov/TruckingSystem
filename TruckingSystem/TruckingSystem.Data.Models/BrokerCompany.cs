using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static TruckingSystem.Common.ValidationConstants.BrokerCompanyConstants;

namespace TruckingSystem.Data.Models
{
    public class BrokerCompany
    {
        public BrokerCompany()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique Identifier")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Broker company name")]
        [MaxLength(CompanyNameMaxLength)]
        public string CompanyName { get; set; } = null!;

        public ICollection<Load> Loads { get; set; } = new HashSet<Load>();

        public ICollection<DriverManager> DriverManagers { get; set; } = new HashSet<DriverManager>();

        [Comment("Shows weather Broker Company is deleted or not")]
        public bool IsDeleted { get; set; }
    }
}
