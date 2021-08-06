using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgApp2.Repositories
{
    public class TaakRepository : ITaakRepository
    {
        private readonly ZorgAppDbContext _context;
        public TaakRepository(ZorgAppDbContext context)
        {
            _context = context;
        }
        public Taak taak { get; set; }
        public List<Taak> taken { get; set; }
        public KSTaak kstaak { get; set; }

        public Taak OphalenTaak(int id)
        {
            taak = _context.Taak.FirstOrDefault(u => u.Id == id);
            return taak;
        }

        //De taken uit de algemene takenlijst.
        //Deze moeten verschijnen in een dropdown op de edit klant pagina en op de bezoekplanningspagina.
        public List<Taak> OphalenTaken()
        {
            taken = _context.Taak.ToList();
            return taken;
        }

        public KSTaak ToevoegenKSTaak(int klantId, int taakId)
        {
            kstaak = new KSTaak()
            {
                TaakId = taakId,
                KlantId = klantId
            };
            
            _context.KSTaak.Add(kstaak);
            _context.SaveChanges();
            return kstaak;
        }

        public KSTaak VerwijderKSTaak(int kstaakId)
        {
            System.Diagnostics.Debug.WriteLine(kstaakId);

            kstaak = _context.KSTaak.Find(kstaakId);

            if (kstaak != null)
            {
                _context.KSTaak.Remove(kstaak);
                _context.SaveChanges();
            }

            return kstaak;
        }
    }
}
