using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RentAPI.Helper;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcjenasController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public OcjenasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public ActionResult<List<Ocjena>> GetAllOcjene()
        {
            var data = _applicationDbContext.Ocjenas
                .Include(x => x.Korisnik)
                .Include(x => x.Automobil)
                .OrderBy(x => x.OcjenaId).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOcjena([FromRoute] int id)
        {
            var ocjena = _applicationDbContext.Ocjenas
                .Include(x => x.Korisnik)
                .Include(x => x.Automobil)
                .FirstOrDefault(x => x.OcjenaId == id);

            if (ocjena == null)
                return BadRequest("Pogresan ID");

            return Ok(ocjena);
        }

        [HttpPost]
        public ActionResult<Ocjena> AddOcjena([FromBody] OcjenaVM x)
        {
            var newOcjena = new Ocjena
            {
                BrojOcjene = x.BrojOcjene,
                DatumOcjene = x.DatumOcjene,
                KorisnikId = x.KorisnikId,
                AutomobilId = x.AutomobilId,
            };

            _applicationDbContext.Add(newOcjena);
            _applicationDbContext.SaveChanges();

            return newOcjena;
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateOcjena([FromRoute] int id, OcjenaVM x)
        {
            var ocjena = _applicationDbContext.Ocjenas.Find(id);

            if (ocjena == null)
            {
                return BadRequest("Pogresan ID");

            }

            ocjena.BrojOcjene = x.BrojOcjene;
            ocjena.DatumOcjene = x.DatumOcjene;
            ocjena.AutomobilId = x.AutomobilId;
            

            _applicationDbContext.SaveChanges();
            return Ok(ocjena);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOcjena([FromRoute] Guid id)
        {
            var ocjena = await _applicationDbContext.Ocjenas.FindAsync(id);


            if (ocjena == null)
            {
                return NotFound();
            }

            _applicationDbContext.Ocjenas.Remove(ocjena);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(ocjena);
        }

        
    }
}
