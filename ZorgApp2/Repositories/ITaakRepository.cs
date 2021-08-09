using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgApp2.Repositories
{
    public interface ITaakRepository
    {
        List<Taak> OphalenTaken();
        Taak OphalenTaak(int Id);
        KSTaak ToevoegenKSTaak(int klantId, int taakId);
        KSTaak VerwijderKSTaak(int kstaakId);
        Handeling ToevoegenHandeling(int bezoekId, int taakId);
    }
}
