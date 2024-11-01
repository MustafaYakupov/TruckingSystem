using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TruckingSystem.Data.Models
{
    public class CompletedDispatch
    {
        public CompletedDispatch()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Complete dispatch identifier")]
        public Guid Id { get; set; }

        public ICollection<Dispatch> Dispatches { get; set; } = new HashSet<Dispatch>();
    }
}
