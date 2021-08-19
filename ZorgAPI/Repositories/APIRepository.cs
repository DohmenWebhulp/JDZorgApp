using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgAPI.Data;
using Microsoft.EntityFrameworkCore;
using ZorgApp2.Models;
namespace ZorgAPI.Repositories
{
    public class APIRepository : IAPIRepository
    {
        private readonly ZorgAPIContext _context;
        public APIRepository(ZorgAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Handeling>> Get()
        {
            return await _context.Handeling.ToListAsync();
        }

        public async Task<Handeling> Get(int Id)
        {
            return await _context.Handeling.FindAsync(Id);
        }

        public async Task<Klant> OphalenKlant(int klantId)
        {
            return await _context.Klanten.FindAsync(klantId);
        }
        public async Task<Taak> OphalenTaak(int taakId)
        {
            return await _context.Taak.FindAsync(taakId);
        }

        public async Task<Medewerker> OphalenMedewerker(int Id)
        {
            return await _context.Medewerker.Include(d => d.Bezoeken).ThenInclude(d => d.Handelingen).FirstOrDefaultAsync(d => d.Id == Id);
        }

        public async Task UpdateHandeling(Handeling Handeling)
        {
            _context.Entry(Handeling).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBezoek(Bezoek Bezoek)
        {
            _context.Entry(Bezoek).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
