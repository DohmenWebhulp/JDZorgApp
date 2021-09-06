using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgAPI.Data
{
    public class ZorgAPIContext : DbContext
    {
        public ZorgAPIContext(DbContextOptions<ZorgAPIContext> options) : base(options)
        {

        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Taak> Taak { get; set; }
        public DbSet<Medewerker> Medewerker { get; set; }
        public DbSet<KSTaak> KSTaak { get; set; }
        public DbSet<Handeling> Handeling { get; set; }
        public DbSet<Bezoek> Bezoek { get; set; }
    }
}
