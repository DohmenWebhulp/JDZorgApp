using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgApp2.Repositories
{
    public interface IKlantRepository
    {
        List<Klant> OphalenKlanten();
        Klant OphalenKlant(int Id);
        Klant UpdateKlant(Klant Klant);
        Klant VerwijderKlant(int Id);

    }
}
