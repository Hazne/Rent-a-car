using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Helper;
using RentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervisanjesController : Controller
    {
        
        public readonly ApplicationDbContext _applicationDbContext;
       
        public RezervisanjesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
           
        }

        [HttpGet("{id}")]
        public ActionResult GetProvjeraDatuma(int id)
        {
            var rezervacija = _applicationDbContext.Rezervisanjes.Where(x=>x.AutomobilId== id).OrderBy(x=>x.DatumZavrsetka).LastOrDefault();


            return Ok(rezervacija);
        }


        [HttpGet]

        public ActionResult<List<Rezervisanje>> GetAllRezervisanjes()
        {
            var rezervisanjes = _applicationDbContext.Rezervisanjes
                //.Include(s => s.Korisnik)
                //.Include(s => s.Automobil)
                .OrderBy(s => s.RezervisanjeId).AsQueryable();

            return rezervisanjes.Take(100).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddRezervisanje([FromBody] Rezervisanje rezervisanjeRequest)
        {

            await _applicationDbContext.Rezervisanjes.AddAsync(rezervisanjeRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(rezervisanjeRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRezervisanje([FromRoute] int id)
        {
            var rezervisanje = await _applicationDbContext.Rezervisanjes.FirstOrDefaultAsync(x => x.RezervisanjeId == id);

            if (rezervisanje == null)
                return NotFound();

            return Ok(rezervisanje);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateRezervisanje(int id,[FromBody] Rezervisanje updateRezervisanjeRequest)
        {
            Rezervisanje rezervisanje;

            if (id == 0)
            {
                rezervisanje = new Rezervisanje();
                _applicationDbContext.Add(rezervisanje);
            }
            else
            {
                rezervisanje = _applicationDbContext.Rezervisanjes.Find(id);
                if (rezervisanje == null)
                {
                    return BadRequest("Ne postoji ID");
                }
            }

            rezervisanje.StatusOcjene = updateRezervisanjeRequest.StatusOcjene;
            rezervisanje.StatusKomentara = updateRezervisanjeRequest.StatusKomentara;
            rezervisanje.Automobil = updateRezervisanjeRequest.Automobil;
            rezervisanje.AutomobilId = updateRezervisanjeRequest.AutomobilId;
            rezervisanje.DatumPocetka = updateRezervisanjeRequest.DatumPocetka;
            rezervisanje.DatumZavrsetka = updateRezervisanjeRequest.DatumZavrsetka;
            rezervisanje.Korisnik = updateRezervisanjeRequest.Korisnik;
            rezervisanje.KorisnikId = updateRezervisanjeRequest.KorisnikId;

            _applicationDbContext.SaveChanges();
            return Ok(rezervisanje);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRezervisanje( int id)
        {
            var rezervisanje = _applicationDbContext.Rezervisanjes.Find(id);


            if (rezervisanje == null)
            {
                return NotFound();
            }

            _applicationDbContext.Rezervisanjes.Remove(rezervisanje);
            _applicationDbContext.SaveChanges();
            return Ok(rezervisanje);
        }
    }
}
