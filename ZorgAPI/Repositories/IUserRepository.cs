using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgAPI.Models;

namespace ZorgAPI.Repositories
{
    public interface IUserRepository
    {
        public Task OpslaanUser(string gebruiker, string wachtwoord);
        public UserMobile OphalenUser(string gebruiker);
        public bool CheckUser(Guid? id, UserMobile user, string wachtwoord);
    }
}
