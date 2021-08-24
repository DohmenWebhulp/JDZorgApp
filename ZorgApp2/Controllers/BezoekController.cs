using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
using ZorgApp2.ViewModel;
using ZorgApp2.Repositories;
using Microsoft.AspNetCore.Identity;
using ZorgApp2.Areas.Identity.Data;
using System.Security.Claims;

namespace ZorgApp2.Controllers
{
    public class BezoekController : Controller
    {
        private IKlantRepository kr { get; set; }
        private ITaakRepository tr { get; set; }
        private IMedewerkerRepository mr { get; set; }
        private IBezoekRepository br { get; set; }

        //private readonly UserManager<Planner> userManager;
        public BezoekController(IKlantRepository _kr, ITaakRepository _tr, 
                                IMedewerkerRepository _mr, IBezoekRepository _br)
                                
        {
            kr = _kr;
            tr = _tr;
            mr = _mr;
            br = _br;
            //userManager = _userManager;
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
            int bezoekId;

            //Omdat .Net moeilijk doet wanneer er geen foreign keys zijn gedefiniëerd bij het aanmaken van een nieuw bezoek,
            //worden er default foreign key id's meegegeven aan het bezoek. Deze kunnen later aangepast worden.

            if (id == null)
            {
                bezoek.KlantId = 11;
                bezoek.MedewerkerId = 1;
                bezoekId = br.ToevoegenBezoek(bezoek).Id;
            }
            else
            {
                bezoekId = id ?? default;
            }
            bezoek = br.OphalenBezoek(bezoekId);
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
        //private Task<Planner> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            System.Diagnostics.Debug.WriteLine(userId);
            var bezoeken = br.OphalenBezoeken();

            //Omdat de bezoeken met klantgegevens op de juiste plaats in de kalender moeten verschijnen, is er een
            //Event entity aangemaakt met een Id, Klantnaam en datum van bezoek als attributen.
            //Deze worden verder niet in een database opgeslagen en zijn puur voor frontend gebruik.

            Event[] events = new Event[bezoeken.Count()];
            
            for (int i = 0; i < bezoeken.Count(); i++)
            {
                events[i] = 
                new Event
                {
                    Id = i,
                    Inhoud = bezoeken[i].Klant.Naam.ToString(),
                    /*KlantNaam = bezoeken[i].Klant.Naam.ToString(),
                    KlantAdres = bezoeken[i].Klant.Adres.ToString(),
                    KlantPostcode = bezoeken[i].Klant.Postcode.ToString(),
                    KlantWoonplaats = bezoeken[i].Klant.Woonplaats.ToString(),*/
                    StartDatum = bezoeken[i].Datum.ToString()
                };
            }
            ViewData["events"] = events;
            return View(bezoeken);
        }

        //Bij het doorgeven van het BPVM geeft het de informatie door die ofwel
        //na het nieuw aanmaken van het bezoek, ofwel na de laatste save van een bezoek (knop Opslaan)
        //voor dat bezoek geldden.
        //Dit betekent dat er potentieel verkeerde informatie over de klantId en/of medewerkerId wordt doorgegeven.
        //De informatie die hoort bij de selectie uit de dropdowns in de view moet eigenlijk worden doorgegeven.
        //Deze corresponderen met de klantId en mwId.
        //Deze worden onder andere gebruikt om het doorgegeven bezoek, wat alleen een id bevat,
        //van de juiste info te voorzien.
        
        [HttpPost]
        public IActionResult KlantDoorgeven(BezoekPlanViewModel bpv, int klantId)
        {
            Klant klant = kr.OphalenKlant(klantId);
            foreach(KSTaak kstaak in klant.KSTaken)
            {
                tr.ToevoegenHandeling(bpv.Bezoek.Id, kstaak.TaakId);
            }
            bpv.Bezoek.Klant = klant;
            Bezoek bezoek = br.OphalenBezoek(bpv.Bezoek.Id);
            Medewerker mw = mr.OphalenMedewerker(bezoek.MedewerkerId);
            bpv.Bezoek.Medewerker = mw;
            bpv.Bezoek.MedewerkerId = bezoek.MedewerkerId;
            bpv.Bezoek.KlantId = klantId;
            br.UpdateBezoek(bpv.Bezoek);
            return RedirectToAction("MaakBezoek", new { id = bpv.Bezoek.Id });
        }

        [HttpPost]
        public IActionResult MwDoorgeven(BezoekPlanViewModel bpv, int mwId)
        {
            Medewerker mw = mr.OphalenMedewerker(mwId);
            bpv.Bezoek.Medewerker = mw;
            Bezoek bezoek = br.OphalenBezoek(bpv.Bezoek.Id);
            Klant klant = kr.OphalenKlant(bezoek.KlantId);
            bpv.Bezoek.Klant = klant;
            bpv.Bezoek.MedewerkerId = mwId;
            bpv.Bezoek.KlantId = bezoek.KlantId;
            br.UpdateBezoek(bpv.Bezoek);
            return RedirectToAction("MaakBezoek", new { id = bpv.Bezoek.Id });
        }

        [HttpPost]
        public IActionResult UpdateBezoek(BezoekPlanViewModel bpv, int mwId, int klantId)
        {
            bpv.Bezoek.KlantId = klantId;
            bpv.Bezoek.MedewerkerId = mwId;
            br.UpdateBezoek(bpv.Bezoek);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VerwijderBezoek(Bezoek bezoek)
        {
            br.VerwijderBezoek(bezoek.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToevoegenHandeling(BezoekPlanViewModel model, int taakId)
        {
            var bezoekId = model.Bezoek.Id;
            tr.ToevoegenHandeling(bezoekId, taakId);
            return RedirectToAction("MaakBezoek", new { id = bezoekId });
        }

        [HttpPost]
        public IActionResult VerwijderHandeling(Handeling handeling)
        {
            var delhand = tr.VerwijderHandeling(handeling.Id);

            return RedirectToAction("MaakBezoek", new { id = delhand.BezoekId });
        }
    }
}
