using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Repositories;
using ZorgApp2.ViewModel;
using ZorgApp2.Models;
namespace ZorgApp2.Controllers
{
    public class MedewerkerController : Controller
    {
        private IMedewerkerRepository mr { get; set; }
        public MedewerkerController(IMedewerkerRepository _mr)
        {
            mr = _mr;
        }
        public IActionResult Index()
        {
            var mws = mr.OphalenMedewerkers();
            return View(mws);
        }

        public IActionResult EditMedewerker(int id)
        {
            var mw = mr.OphalenMedewerker(id);
            return View(mw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMedewerker(Medewerker Medewerker)
        {
            mr.UpdateMedewerker(Medewerker);
            return RedirectToAction("Index");
        }

        public IActionResult ToevoegenMedewerkerForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ToevoegenMedewerker(Medewerker Medewerker)
        {
            mr.ToevoegenMedewerker(Medewerker);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VerwijderMedewerker(Medewerker Medewerker)
        {
            mr.VerwijderMedewerker(Medewerker.Id);
            return RedirectToAction("Index");
        }
    }
}
