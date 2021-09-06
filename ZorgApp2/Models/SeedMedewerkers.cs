using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgApp2.Models
{
    public static class SeedMedewerkers
    {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new ZorgAppDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<ZorgAppDbContext>>()))
                {
                    if (context.Medewerker.Any())
                    {
                        return;
                    }

                    context.Medewerker.AddRange(
                        new Medewerker
                        {
                            GUID = Guid.NewGuid(),
                            Gebruikersnaam = "Carla Peters",
                            Wachtwoord = BCrypt.Net.BCrypt.HashPassword("mw1zapp!")
                        },

                        new Medewerker
                        {
                            GUID = Guid.NewGuid(),
                            Gebruikersnaam = "Benny Jarig",
                            Wachtwoord = BCrypt.Net.BCrypt.HashPassword("mw2zapp!")
                        },

                        new Medewerker
                        {
                            GUID = Guid.NewGuid(),
                            Gebruikersnaam = "Kenny Fietsen",
                            Wachtwoord = BCrypt.Net.BCrypt.HashPassword("mw3zapp!")
                        }
                    );
                    context.SaveChanges();
                    Console.WriteLine("Seeding");
                }
            }
    }
}
