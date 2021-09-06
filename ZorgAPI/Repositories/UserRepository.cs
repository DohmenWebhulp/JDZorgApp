using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgAPI.Data;
using ZorgApp2.Models;

namespace ZorgAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ZorgAPIContext _context;
        public UserRepository(ZorgAPIContext context)
        {
            _context = context;
        }

        public bool CheckUser(Medewerker user, string wachtwoord)
        {
            if (user == null || BCrypt.Net.BCrypt.Verify(wachtwoord, user.Wachtwoord))               
            {
                return false;
            }
            return true;
        }

        public bool CheckGuid(Medewerker user, Guid guid)
        {
            if(user.GUID != guid && user.GUID != null)
            {
                return false;
            }
            return true;
        }

        public Medewerker OphalenMedewerkerOpUsername(string gebruiker)
        {
            return _context.Medewerker.Include(m => m.Bezoeken).ThenInclude(b => b.Handelingen).ThenInclude(h => h.Taak)
                                            .Include(m => m.Bezoeken).ThenInclude(b => b.Klant)
                                            .FirstOrDefault(m => m.Gebruikersnaam == gebruiker);
        }
        public Medewerker OphalenMedewerkerOpId(int id)
        {
            return _context.Medewerker.Include(m => m.Bezoeken).ThenInclude(b => b.Handelingen).ThenInclude(h => h.Taak)
                                            .Include(m => m.Bezoeken).ThenInclude(b => b.Klant)
                                            .FirstOrDefault(m => m.Id == id);
        }

        public Medewerker OphalenUser(string gebruiker)
        {
            var user = _context.Medewerker.Find(gebruiker);
            return user;
        }

        public Medewerker OphalenUser2(int id)
        {
            var user = _context.Medewerker.Find(id);
            return user;
        }
    }
}
