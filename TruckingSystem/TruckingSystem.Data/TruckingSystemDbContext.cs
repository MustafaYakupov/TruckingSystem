using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TruckingSystem.Data
{
    public class TruckingSystemDbContext : IdentityDbContext
    {
        public TruckingSystemDbContext(DbContextOptions<TruckingSystemDbContext> options)
            : base(options)
        {
        }
    }
}
