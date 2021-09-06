using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgAPI.Repositories;
using ZorgApp2.Models;

namespace ZorgAPI.Controllers
{
    [Route("user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository ur;

        public UserController(IUserRepository _ur)
        {
            ur = _ur;
        }
        //Via een JSon object komen de guid, gebruikersnaam en wachtwoord van de medewerker binnen.
        //Bij eerste gebruik worden de opgegeven gebruikersnaam en wachtwoord gecheckt.
        //Bij ieder gebruik in de 5 dagen daarna wordt de token gevraagd voor verificatie.
        //Dit gebeurt zonder dat de medewerker hoeft in te loggen.
        //Deze wordt dan vergeleken met de token die in de database staat.
        //Is er onjuiste informatie verstrekt, volgt een error en kan de medewerker niet verder.
        [HttpPut("{id}")]
        [Route("auth/{id}")]
        public IActionResult Authenticeren(int id, Medewerker mw)
        {
            System.Diagnostics.Debug.WriteLine("Hello");
            var gebruiker = mw.Gebruikersnaam;
            var wachtwoord = mw.Wachtwoord;
            var guid = mw.GUID;
            var user = new Medewerker();
            if (id != 0) //gebruiker is null wanneer token 5 dagen valide is.
            {
                user = ur.OphalenMedewerkerOpId(id);
                if (!ur.CheckGuid(user, guid))
                {
                    return BadRequest();
                }
            }
            else //Eerste keer inloggen
            {
                user = ur.OphalenMedewerkerOpUsername(gebruiker);
                if (!ur.CheckUser(user, wachtwoord))
                {
                    return BadRequest();
                }
            }
            var token = new JsonResult(user);
            return token;
        }
        //Dit is om de medewerkerinformatie op te halen voor testgebruik wat betreft de Authenticeren functie 
        [HttpGet("{id}")]
        [Route("mw2/{id}")]
        public ActionResult<Medewerker> GetMedewerker(int id)
        {
            var mw = ur.OphalenUser2(id);

            if (mw == null)
            {
                return NotFound();
            }

            return mw;
        }
    }
}
