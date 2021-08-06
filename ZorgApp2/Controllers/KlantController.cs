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

        //De taken die horen bij een specifieke klant.
        //Deze moeten toegevoegd worden en uiteindelijk ook verschijnen op de edit klant pagina.

        [HttpPost]
        public IActionResult ToevoegenKSTaak(KlantViewModel Model, int taakId)
        {
            var klantId = Model.Klant.Id;
            var bol = tr.ToevoegenKSTaak(klantId, taakId);
            return RedirectToAction("EditKlant", new { id = klantId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateKlant(Klant Klant)
        {
            kr.UpdateKlant(Klant);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VerwijderKlant(int klantId)
        {
            kr.VerwijderKlant(klantId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VerwijderKSTaak(int kstaakId)
        {
            var kstaak = tr.VerwijderKSTaak(kstaakId);

            return RedirectToAction("EditKlant", new { id = kstaak.KlantId });
        }

    }
}
