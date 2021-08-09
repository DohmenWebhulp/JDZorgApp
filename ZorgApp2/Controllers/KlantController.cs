using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
using ZorgApp2.ViewModel;
using ZorgApp2.Repositories;
namespace ZorgApp2.Controllers
{
    public class KlantController : Controller
    {
        private IKlantRepository kr { get; set; }
        private ITaakRepository tr { get; set; }
        public KlantController(IKlantRepository _kr, ITaakRepository _tr)
        {
            kr = _kr;
            tr = _tr;
        }
        
        public KlantViewModel Model { get; set; }

        public IActionResult EditKlant(int id)
        {
            var klant = kr.OphalenKlant(id);
            var taken = tr.OphalenTaken();

            Model = new KlantViewModel()
            {
                Klant = klant,
                Taken = taken
            };
            return View(Model);
        }

        public IActionResult Index()
        {
            var Klanten = kr.OphalenKlanten();
            return View(Klanten);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateKlant(Klant klant)
        {
            kr.UpdateKlant(klant);
            return RedirectToAction("Index");
        }

        public IActionResult ToevoegenKlantForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ToevoegenKlant(Klant klant)
        {
            kr.ToevoegenKlant(klant);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VerwijderKlant(Klant klant)
        {
            kr.VerwijderKlant(klant.Id);
            return RedirectToAction("Index");
        }

        //De taken die horen bij een specifieke klant.
        //Deze moeten toegevoegd worden en uiteindelijk ook verschijnen op de edit klant pagina.

        [HttpPost]
        public IActionResult ToevoegenKSTaak(KlantViewModel model, int taakId)
        {
            var klantId = model.Klant.Id;
            tr.ToevoegenKSTaak(klantId, taakId);
            return RedirectToAction("EditKlant", new { id = klantId });
        }

        [HttpPost]
        public IActionResult VerwijderKSTaak(KSTaak kstaak)
        {
            var delkstaak = tr.VerwijderKSTaak(kstaak.Id);

            return RedirectToAction("EditKlant", new { id = delkstaak.KlantId });
        }

    }
}
