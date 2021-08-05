using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;

namespace ZorgApp2.Controllers
{
    public class RegelTakenController : Controller
    {
        private readonly ZorgAppDbContext _context;
        public RegelTakenController(ZorgAppDbContext context)
        {
            _context = context;
        }
        public Klant Klant { get; set; }
        public List<Taak> Taken { get; set; }
        public Taak Taak { get; set; }
        public KSTaak KSTaak { get; set; }

        public async Task<IActionResult> Index(int? id)
        {
            Klant = await OphalenKlant(id);

            Taken = await OphalenTaken();

            ViewData["Taken"] = Taken;

            return View(Klant);
        }

        //De taken uit de algemene takenlijst.
        //Deze moeten verschijnen in een dropdown op de edit klant pagina en op de bezoekplanningspagina.
        private async Task<List<Taak>> OphalenTaken()
        {
            Taken = await _context.Taak.ToListAsync();
            return Taken;
        }

        private async Task<Klant> OphalenKlant(int? id)
        {
            Klant = await _context.Klanten.FirstOrDefaultAsync(u => u.Id == id);
            return Klant;
        }

        //De taken die horen bij een specifieke klant.
        //Deze moeten toegevoegd worden en uiteindelijk ook verschijnen op de edit klant pagina.

        [HttpPost]
        public IActionResult ToevoegenKSTaak()
        {
            KSTaak = new KSTaak();
            KSTaak.Extra_info = Taak.Extra_info;
            KSTaak.KlantId = Klant.Id;
            _context.KSTaak.Add(KSTaak);
            return View();
        }

        [HttpPost]
        public IActionResult UpdateKlant()
        {
            _context.Klanten.Update(Klant);
            return View();
        }

    }
}
