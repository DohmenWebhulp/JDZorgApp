using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public class ZorgAppDbContext : DbContext
    {
        public ZorgAppDbContext(DbContextOptions<ZorgAppDbContext> options) : base(options)
        {

        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Taak> Taak { get; set; }
        public DbSet<Planner> Planner { get; set; }
        public DbSet<Medewerker> Medewerker { get; set; }
        public DbSet<KSTaak> KSTaak { get; set; }
        public DbSet<Handeling> Handeling { get; set; }
        public DbSet<Bezoek> Bezoek { get; set; }
    }
}
