using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{

    public class AtelierOniriumContext : DbContext
    {
        public AtelierOniriumContext(DbContextOptions<AtelierOniriumContext> options)
            : base(options)
        {
        }
        public DbSet<Creation> Creations { get; set; }
    }
}
