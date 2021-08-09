using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgApp2.Repositories
{
    public interface IBezoekRepository
    {
        List<Bezoek> OphalenBezoeken();
        Bezoek OphalenBezoek(int? Id);
        Bezoek UpdateBezoek(Bezoek Bezoek);
        Bezoek ToevoegenBezoek(Bezoek Bezoek);
        Bezoek VerwijderBezoek(int Id);
    }
}
