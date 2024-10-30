using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TruckingSystem.Data
{
    public class TruckingSystemDbContext : IdentityDbContext
    {
        public TruckingSystemDbContext()
        {   
        }
        public TruckingSystemDbContext(DbContextOptions<TruckingSystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
