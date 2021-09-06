using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;

namespace ZorgAPI.Repositories
{
    public interface IUserRepository
    {
        public Medewerker OphalenUser(string gebruiker);
        public Medewerker OphalenUser2(int id);
        public Medewerker OphalenMedewerkerOpUsername(string gebruiker);
        public Medewerker OphalenMedewerkerOpId(int id);
        public bool CheckUser(Medewerker user, string wachtwoord);
        public bool CheckGuid(Medewerker user, Guid guid);
    }
}
