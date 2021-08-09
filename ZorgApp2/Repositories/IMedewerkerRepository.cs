using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgApp2.Repositories
{
    public interface IMedewerkerRepository
    {
        public Medewerker OphalenMedewerker(int Id);
        public List<Medewerker> OphalenMedewerkers();
        public Medewerker UpdateMedewerker(Medewerker medewerker);
        public Medewerker ToevoegenMedewerker(Medewerker Medewerker);
        public Medewerker VerwijderMedewerker(int mwId);
        
    }
}
