using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgAPI.Data;
using Microsoft.EntityFrameworkCore;
using ZorgApp2.Models;
using Microsoft.AspNetCore.Identity;

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
        public async Task<Medewerker> OphalenMedewerker(int Id)
        {
            return await _context.Medewerker.Include(m => m.Bezoeken).ThenInclude(b => b.Handelingen).ThenInclude(h => h.Taak)
                                            .Include(m => m.Bezoeken).ThenInclude(b => b.Klant)
                                            .FirstOrDefaultAsync(d => d.Id == Id);
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
