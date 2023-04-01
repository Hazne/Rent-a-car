using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Helper;
using RentAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KomentarsController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public KomentarsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public ActionResult<List<Komentar>> GetAllKomentars()
        {
            var data = _applicationDbContext.Komentars
                .OrderBy(x => x.KomentarId)
                .AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<Komentar> AddKomentar([FromBody] KomentarVM x)
        {
            var newKomentar = new Komentar()
            {
                Opis = x.Opis,
                DatumKomentara = x.DatumKomentara,
                KorisnikId = x.KorisnikId,
                AutomobilId = x.AutomobilId,
                RezervisanjeId= x.RezervisanjeId,
            };

            StatusKomentara(newKomentar.RezervisanjeId);
            _applicationDbContext.Add(newKomentar);
            _applicationDbContext.SaveChanges();
            return newKomentar;
        }

        private void StatusKomentara(int rezervisanjeId)
        {
            var rezervisanje = _applicationDbContext.Rezervisanjes.Find(rezervisanjeId);

            rezervisanje.StatusKomentara = true; 
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetKomentar([FromRoute] int id)
        {
            var komentar = _applicationDbContext.Komentars.FirstOrDefault(x => x.KomentarId == id);

            if (komentar == null)
                return BadRequest("Pogresan Id");

            return Ok(komentar);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateKomentar(int id,[FromBody] Komentar updateKomentarRequest)
        {
            Komentar komentar;

            if (id == 0)
            {
                komentar = new Komentar()
                {
                    DatumKomentara = DateTime.Now
                };
                _applicationDbContext.Add(komentar);
            }
            else
            {
                komentar = _applicationDbContext.Komentars.Find(id);
                if (komentar == null)
                {   
                    return BadRequest("Pogresan Id");
                }
            }

            komentar.Automobil = updateKomentarRequest.Automobil;
            komentar.AutomobilId = updateKomentarRequest.AutomobilId;
            komentar.Opis = updateKomentarRequest.Opis;
            komentar.KorisnikId = updateKomentarRequest.KorisnikId;
            komentar.Korisnik = updateKomentarRequest.Korisnik;
            komentar.RezervisanjeId = updateKomentarRequest.RezervisanjeId;
            komentar.Rezervisanje = updateKomentarRequest.Rezervisanje;

            StatusKomentara(komentar.RezervisanjeId);
            _applicationDbContext.SaveChanges();
            return Ok(komentar);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteKomentar(int id)
        {
            var komentar = _applicationDbContext.Komentars.Find(id);


            if (komentar == null)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(komentar);
            _applicationDbContext.SaveChanges();
            return Ok(komentar);
        }
    }
}
