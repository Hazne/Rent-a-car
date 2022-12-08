using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisniksController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public KorisniksController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllKorisniks()
        {
            var korisniks = await _applicationDbContext.Korisniks.ToListAsync();

            return Ok(korisniks);
        }

        [HttpPost]
        public async Task<IActionResult> AddKorisnik([FromBody] Korisnik korisnikRequest)
        {

            await _applicationDbContext.Korisniks.AddAsync(korisnikRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(korisnikRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetKorisnik([FromRoute] int id)
        {
            var korisnik = await _applicationDbContext.Korisniks.FirstOrDefaultAsync(x => x.KorisnikId == id);

            if (korisnik == null)
                return NotFound();

            return Ok(korisnik);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateKorisnik([FromRoute] int id, Korisnik updateKorisnikRequest)
        {
            var korisnik = await _applicationDbContext.Korisniks.FindAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            korisnik.Ime = updateKorisnikRequest.Ime;
            korisnik.Prezime = updateKorisnikRequest.Prezime;
            korisnik.Email = updateKorisnikRequest.Email;
            korisnik.BrojMobitela = updateKorisnikRequest.BrojMobitela;
            korisnik.Username = updateKorisnikRequest.Username;
            korisnik.Ocjena = updateKorisnikRequest.Ocjena;
            korisnik.Grad = updateKorisnikRequest.Grad;
            korisnik.TipKorisnika = updateKorisnikRequest.TipKorisnika;
            korisnik.Komentar = updateKorisnikRequest.Komentar;
            korisnik.Rezervisanje = updateKorisnikRequest.Rezervisanje;


            await _applicationDbContext.SaveChangesAsync();
            return Ok(korisnik);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteKorisnik([FromRoute] int id)
        {
            var korisnik = await _applicationDbContext.Korisniks.FindAsync(id);


            if (korisnik == null)
            {
                return NotFound();
            }

            _applicationDbContext.Korisniks.Remove(korisnik);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(korisnik);
        }
    }
}
