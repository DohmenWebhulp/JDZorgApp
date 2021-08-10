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
        Taak UpdateTaak(Taak Taak);
        Taak ToevoegenTaak(Taak Taak);
        Taak VerwijderTaak(int mwId);
        KSTaak ToevoegenKSTaak(int klantId, int taakId);
        KSTaak VerwijderKSTaak(int kstaakId);
        Handeling ToevoegenHandeling(int bezoekId, int taakId);
        Handeling VerwijderHandeling(int handId);
    }
}
