using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgAPI.Repositories
{
    public interface IAPIRepository
    {
        public Task<IEnumerable<Handeling>> Get();
        public Task<Handeling> Get(int Id);
        public Task UpdateHandeling(Handeling Handeling);
        public Task UpdateBezoek(Bezoek Bezoek);
        public Task<Medewerker> OphalenMedewerker(int Id);
        
    }
}
