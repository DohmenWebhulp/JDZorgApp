using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgAPI.Data;
using ZorgAPI.Models;

namespace ZorgAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ZorgAPIContext _context;
        public UserRepository(ZorgAPIContext context)
        {
            _context = context;
        }
        public async Task OpslaanUser(string gebruiker, string wachtwoord)
        {
            UserMobile user = new UserMobile
            {
                Id = Guid.NewGuid(),
                Gebruikersnaam = gebruiker,
                Wachtwoord = BCrypt.Net.BCrypt.HashPassword(wachtwoord)
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public bool CheckUser(Guid? id, UserMobile user, string wachtwoord)
        {
            if (user == null || !BCrypt.Net.BCrypt.Verify(wachtwoord, user.Wachtwoord) 
                             || (user.Id != id && user.Id != null))
            {
                return false;
            }
            return true;
        }
        public UserMobile OphalenUser(string gebruiker)
        {
            var user = _context.Users.Find(gebruiker);
            return user;
        }
    }
}
