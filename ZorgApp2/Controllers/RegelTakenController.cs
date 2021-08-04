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
        public List<Taak> Taken { get; set; }
        public List<KSTaak> KSTaken { get; set; }
        public KSTaak KSTaak { get; set; }

        public async Task<IActionResult> Index()
        {
            await OphalenTaken();
            await OphalenKSTaken();
            return View();
        }

        //De taken uit de algemene takenlijst.
        //Deze moeten verschijnen in een dropdown op de edit klant pagina en op de bezoekplanningspagina.
        private async Task OphalenTaken()
        {
            Taken = await _context.Taak.ToListAsync();
        }

        //De taken die horen bij een specifieke klant.
        //Deze moeten toegevoegd worden en uiteindelijk ook verschijnen op de edit klant pagina.
        private async Task OphalenKSTaken()
        {
            KSTaken = await _context.KSTaak.ToListAsync();
        }

        [HttpPost]
        public IActionResult ToevoegenKSTaak()
        {
            _context.KSTaak.Add(KSTaak);
            return View();
        }

    }
}
