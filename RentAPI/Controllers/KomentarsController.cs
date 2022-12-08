using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
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

        public async Task<IActionResult> GetAllKomentars()
        {
            var komentars = await _applicationDbContext.Komentars.ToListAsync();

            return Ok(komentars);
        }

        [HttpPost]
        public async Task<IActionResult> AddKomentar([FromBody] Komentar komentarRequest)
        {

            await _applicationDbContext.Komentars.AddAsync(komentarRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(komentarRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetKomentar([FromRoute] int id)
        {
            var komentar = await _applicationDbContext.Komentars.FirstOrDefaultAsync(x => x.KomentarId == id);

            if (komentar == null)
                return NotFound();

            return Ok(komentar);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateKomentar([FromRoute] int id, Komentar updateKomentarRequest)
        {
            var komentar = await _applicationDbContext.Komentars.FindAsync(id);

            if (komentar == null)
            {
                return NotFound();
            }

            komentar.Automobil = updateKomentarRequest.Automobil;
            komentar.Opis = updateKomentarRequest.Opis;
            komentar.DatumKomentara = updateKomentarRequest.DatumKomentara;

            await _applicationDbContext.SaveChangesAsync();
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
