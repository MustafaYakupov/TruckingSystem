using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckingSystem.Data;
using TruckingSystem.Data.Models;
using TruckingSystem.Infrastructure.Repositories.Contracts;

namespace TruckingSystem.Infrastructure.Repositories
{
    public class LoadRepository : Repository<Load>, ILoadRepository
    {
        private readonly TruckingSystemDbContext context;

        public LoadRepository(TruckingSystemDbContext context) 
            : base(context)
        {
            this.context = context;
        }
    }
}
