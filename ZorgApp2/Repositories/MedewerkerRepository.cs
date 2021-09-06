using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgApp2.Repositories
{
    public class MedewerkerRepository : IMedewerkerRepository
    {
        private readonly ZorgAppDbContext _context;

        public MedewerkerRepository(ZorgAppDbContext context)
        {
            _context = context;
        }

        public Medewerker OphalenMedewerker(int mwId)
        {
            Medewerker mw = _context.Medewerker.FirstOrDefault(mw => mw.Id == mwId);
            return mw;
        }

        public List<Medewerker> OphalenMedewerkers()
        {
            List<Medewerker> mws = _context.Medewerker.ToList();
            return mws;
        }

        public Medewerker UpdateMedewerker(Medewerker Medewerker)
        {
            var mw = _context.Medewerker.Attach(Medewerker);
            mw.State = EntityState.Modified;
            _context.SaveChanges();
            return Medewerker;
        }

        public Medewerker ToevoegenMedewerker(Medewerker Medewerker)
        {
            Medewerker mw = new Medewerker
            {
                GUID = Guid.NewGuid(),
                Gebruikersnaam = Medewerker.Gebruikersnaam,
                Wachtwoord = BCrypt.Net.BCrypt.HashPassword(Medewerker.Wachtwoord)
            };
            _context.Medewerker.Add(mw);
            _context.SaveChanges();
            return mw;
        }

        public Medewerker VerwijderMedewerker(int mwId)
        {
            Medewerker mw = _context.Medewerker.Include(c => c.Bezoeken).FirstOrDefault(u => u.Id == mwId);
            if (mw != null)
            {
                foreach (Bezoek bezoek in mw.Bezoeken)
                {
                    _context.Bezoek.Remove(bezoek);
                }
                _context.Medewerker.Remove(mw);
                _context.SaveChanges();
            }
            return mw;
        }
    }
}
