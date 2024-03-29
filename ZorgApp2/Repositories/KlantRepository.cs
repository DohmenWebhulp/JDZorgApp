﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;

namespace ZorgApp2.Repositories
{
    public class KlantRepository : IKlantRepository
    {
        private readonly ZorgAppDbContext _context;
        public KlantRepository(ZorgAppDbContext context)
        {
            _context = context;
        }

        public Klant OphalenKlant(int id)
        {
            Klant klant = _context.Klanten.Include(c => c.KSTaken).FirstOrDefault(u => u.Id == id);
            return klant;
        }

        //Lijst Met Klanten. Voor Beheer Klanten overzicht.
        public List<Klant> OphalenKlanten()
        {
            List<Klant> klanten = _context.Klanten.ToList();
            return klanten;
        }

        public Klant UpdateKlant(Klant Klant)
        {
            var klant = _context.Klanten.Attach(Klant);
            klant.State = EntityState.Modified;
            _context.SaveChanges();
            return Klant;
        }

        public Klant ToevoegenKlant(Klant Klant)
        {
            _context.Klanten.Add(Klant);
            _context.SaveChanges();
            return Klant;
        }

        public Klant VerwijderKlant(int klantId)
        {
            Klant klant = _context.Klanten.Include(c => c.KSTaken).FirstOrDefault(u => u.Id == klantId);
            if (klant != null)
            {
                foreach(KSTaak kstaak in klant.KSTaken)
                {
                    _context.KSTaak.Remove(kstaak);
                }
                foreach(Bezoek bezoek in klant.Bezoeken)
                {
                    _context.Bezoek.Remove(bezoek);
                }
                _context.Klanten.Remove(klant);
                _context.SaveChanges();
            }
            return klant;
        }

    }
}
