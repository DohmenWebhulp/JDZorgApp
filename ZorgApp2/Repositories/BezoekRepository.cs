using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Repositories;
using ZorgApp2.Models;
using ZorgApp2.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ZorgApp2.Repositories
{
    public class BezoekRepository : IBezoekRepository
    {
        private readonly ZorgAppDbContext _context;
        public BezoekRepository(ZorgAppDbContext context)
        {
            _context = context;
        }

        public Bezoek OphalenBezoek(int? id)
        {
            Bezoek Bezoek = _context.Bezoek.Include(c => c.Handelingen).FirstOrDefault(u => u.Id == id);
            return Bezoek;
        }

        //Lijst Met Bezoeken. Voor Bezoeken overzicht.
        public List<Bezoek> OphalenBezoeken()
        {
            List<Bezoek> Bezoeken = _context.Bezoek.ToList();
            return Bezoeken;
        }

        public Bezoek UpdateBezoek(Bezoek Bezoek)
        {
            var bezoek = _context.Bezoek.Attach(Bezoek);
            bezoek.State = EntityState.Modified;
            _context.SaveChanges();
            return Bezoek;
        }

        public Bezoek ToevoegenBezoek(Bezoek Bezoek)
        {
            _context.Bezoek.Add(Bezoek);
            _context.SaveChanges();
            return Bezoek;
        }

        public Bezoek VerwijderBezoek(int BezoekId)
        {
            Bezoek Bezoek = _context.Bezoek.Include(c => c.Handelingen).FirstOrDefault(u => u.Id == BezoekId);
            if (Bezoek != null)
            {
                foreach (Handeling handeling in Bezoek.Handelingen)
                {
                    _context.Handeling.Remove(handeling);
                }
                _context.Bezoek.Remove(Bezoek);
                _context.SaveChanges();
            }
            return Bezoek;
        }
    }
}
