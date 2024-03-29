﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZorgAPI.Data;
using ZorgApp2.Models;
using ZorgAPI.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ZorgAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IAPIRepository ar;

        public APIController(IAPIRepository _ar)
        {
            ar = _ar;
        }

        // GET: api/API/5
        [HttpGet("{id}")]
        [Route("mw/{id}")]
        public async Task<ActionResult<Medewerker>> GetMedewerker(int id)
        {
            var mw = await ar.OphalenMedewerker(id);

            if (mw == null)
            {
                return NotFound();
            }
            
            return mw;
        }

        // PUT: api/API/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Route("handPut/{id}")]
        public async Task<IActionResult> UpdateHandeling(int id, Handeling handeling)
        {
            if (id != handeling.Id)
            {
                return BadRequest();
            }

            await ar.UpdateHandeling(handeling);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Route("bezoekPut/{id}")]
        public async Task<IActionResult> UpdateBezoek(int id, Bezoek bezoek)
        {
            if (id != bezoek.Id)
            {
                return BadRequest();
            }

            await ar.UpdateBezoek(bezoek);

            return NoContent();
        }

        // POST: api/API
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*[HttpPost]
        public async Task<ActionResult<Handeling>> PostHandeling(Handeling handeling)
        {
            _context.Handeling.Add(handeling);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHandeling", new { id = handeling.Id }, handeling);
        }

        // DELETE: api/API/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Handeling>> DeleteHandeling(int id)
        {
            var handeling = await _context.Handeling.FindAsync(id);
            if (handeling == null)
            {
                return NotFound();
            }

            _context.Handeling.Remove(handeling);
            await _context.SaveChangesAsync();

            return handeling;
        }*/
    }
}
