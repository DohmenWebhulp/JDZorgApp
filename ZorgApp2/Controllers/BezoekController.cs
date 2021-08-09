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
    public class BezoekController : Controller
    {
        private IKlantRepository kr { get; set; }
        private ITaakRepository tr { get; set; }
        private IMedewerkerRepository mr { get; set; }
        private IBezoekRepository br { get; set; }
        public BezoekController(IKlantRepository _kr, ITaakRepository _tr, 
                                IMedewerkerRepository _mr, IBezoekRepository _br)
        {
            kr = _kr;
            tr = _tr;
            mr = _mr;
            br = _br;
        }
        public BezoekPlanViewModel BPVmodel { get; set; }

        //Bij het aankomen van het bezoekplanningscherm bestaat er nog geen bezoek.
        //Er is er wel een nodig om mee te werken in het ViewModel en in de View.
        //Daarom wordt er stiekem een bezoek aangemaakt wanneer er geen id gespecificeerd wordt.
        //Als een bezoek uiteindelijk opgeslagen wordt, wordt dit meegegeven aan de UpdateBezoek functie,
        //waardoor de verdere details, die eerst NULL waren, ingevuld kunnen worden.

        public IActionResult MaakBezoek(int? id = null)
        {
            var bezoek = new Bezoek();
            if (id != null)
            {
                bezoek = br.OphalenBezoek(id);
            }
            else
            {
                bezoek.KlantId = 5;
                bezoek.MedewerkerId = 1;
                bezoek.PlannerId = 1;
                br.ToevoegenBezoek(bezoek);
            }
            var klanten = kr.OphalenKlanten();
            var mws = mr.OphalenMedewerkers();
            var taken = tr.OphalenTaken();

            BPVmodel = new BezoekPlanViewModel()
            {
                Klanten = klanten,
                Medewerkers = mws,
                Taken = taken,
                Bezoek = bezoek
            };
            return View(BPVmodel);
        }

        [HttpPost]
        public IActionResult UpdateBezoek(Bezoek bezoek)
        {
            br.UpdateBezoek(bezoek);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToevoegenHandeling(BezoekPlanViewModel model, int taakId)
        {
            var bezoekId = model.Bezoek.Id;
            tr.ToevoegenHandeling(bezoekId, taakId);
            return RedirectToAction("MaakBezoek", new { id = bezoekId });
        }
    }
}
