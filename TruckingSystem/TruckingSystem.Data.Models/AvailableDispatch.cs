using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TruckingSystem.Data.Models
{
    public class AvailableDispatch
    {
        public AvailableDispatch()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Available dispatch identifier")]
        public Guid Id { get; set; }

        public ICollection<Dispatch> Dispatches { get; set; } = new HashSet<Dispatch>();
    }
}
