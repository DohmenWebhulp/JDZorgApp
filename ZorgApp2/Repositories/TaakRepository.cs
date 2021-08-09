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

        public Taak OphalenTaak(int id)
        {
            Taak taak = _context.Taak.FirstOrDefault(u => u.Id == id);
            return taak;
        }

        //De taken uit de algemene takenlijst.
        //Deze moeten verschijnen in een dropdown op de edit klant pagina en op de bezoekplanningspagina.
        public List<Taak> OphalenTaken()
        {
            List<Taak> taken = _context.Taak.ToList();
            return taken;
        }

        public KSTaak ToevoegenKSTaak(int klantId, int taakId)
        {
            KSTaak kstaak = new KSTaak()
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

            KSTaak kstaak = _context.KSTaak.Find(kstaakId);

            if (kstaak != null)
            {
                _context.KSTaak.Remove(kstaak);
                _context.SaveChanges();
            }

            return kstaak;
        }

        public Handeling ToevoegenHandeling(int bezoekId, int taakId)
        {
            Handeling handeling = new Handeling()
            {
                BezoekId = bezoekId,
                TaakId = taakId,
                Uitvoering = false
            };

            _context.Handeling.Add(handeling);
            _context.SaveChanges();
            return handeling;
        }
    }
}
