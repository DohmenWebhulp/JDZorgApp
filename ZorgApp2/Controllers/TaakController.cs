using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgApp2.Models;
using ZorgApp2.Repositories;
using ZorgApp2.ViewModel;
namespace ZorgApp2.Controllers
{
    public class TaakController : Controller
    {
        private ITaakRepository tr { get; set; }
        public TaakController(ITaakRepository _tr)
        {
            tr = _tr;
        }
        public IActionResult Index()
        {
            var mws = tr.OphalenTaken();
            return View(mws);
        }

        public IActionResult EditTaak(int id)
        {
            var mw = tr.OphalenTaak(id);
            return View(mw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTaak(Taak Taak)
        {
            tr.UpdateTaak(Taak);
            return RedirectToAction("Index");
        }

        public IActionResult ToevoegenTaakForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ToevoegenTaak(Taak Taak)
        {
            tr.ToevoegenTaak(Taak);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult VerwijderTaak(Taak Taak)
        {
            tr.VerwijderTaak(Taak.Id);
            return RedirectToAction("Index");
        }
    }
}
