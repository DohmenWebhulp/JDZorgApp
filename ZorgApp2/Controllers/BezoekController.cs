using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
namespace ZorgApp2.Controllers
{
    public class BezoekController : Controller
    {
        private readonly ZorgAppDbContext _context;
        public BezoekController(ZorgAppDbContext context)
        {
            _context = context;
        }
        public List<Klant> Klanten { get; set; }
        public List<Medewerker> Medewerkers { get; set; }

        public async Task<IActionResult> MaakBezoek()
        {
            Klanten = await _context.Klanten.ToListAsync();
            Medewerkers = await _context.Medewerker.ToListAsync();
            ViewData["Klanten"] = Klanten;
            ViewData["Medewerkers"] = Medewerkers;
            return View();
        }
    }
}
