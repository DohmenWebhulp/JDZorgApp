using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZorgAPI.Models;
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
        //Via een JSon object komen de id, gebruikersnaam en wachtwoord van de medewerker binnen.
        //Deze worden vervolgens opgeslagen in de database bij eerste gebruik.
        //Bij elke volgende opstart van de app moet token meegestuurd worden.
        [HttpPost]
        [Route("auth")]
        public IActionResult Authenticate(Guid? id, Medewerker mw)
        {
            var gebruiker = mw.Gebruikersnaam;
            var wachtwoord = mw.Wachtwoord;
            var user = ur.OphalenUser(gebruiker);
            if (!ur.CheckUser(id, user, wachtwoord))
            {
                return BadRequest();
            }
            var token = new JsonResult(user);
            return token;
        }
    }
}
