using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Helper;
using RentAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                .Include(x=>x.Korisnik)
                .Include(x=>x.Automobil)
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
            };

            _applicationDbContext.Add(newKomentar);
            _applicationDbContext.SaveChanges();
            return newKomentar;
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

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateKomentar([FromRoute] int id, Komentar updateKomentarRequest)
        {
            var komentar = _applicationDbContext.Komentars.Find(id);

            if (komentar == null)
            {
                return BadRequest("Pogresan Id");
            }

            komentar.Automobil = updateKomentarRequest.Automobil;
            komentar.Opis = updateKomentarRequest.Opis;
            komentar.DatumKomentara = updateKomentarRequest.DatumKomentara;

            _applicationDbContext.SaveChanges();
            return Ok(komentar);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteKomentar([FromRoute] int id)
        {
            var komentar = await _applicationDbContext.Komentars.FindAsync(id);


            if (komentar == null)
            {
                return NotFound();
            }

            _applicationDbContext.Komentars.Remove(komentar);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(komentar);
        }
    }
}
