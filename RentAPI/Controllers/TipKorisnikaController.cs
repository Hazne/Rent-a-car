using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipKorisnikaController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public TipKorisnikaController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllTipKorisnikas()
        {
            var tipKorisnikas = await _applicationDbContext.TipKorisnikas.ToListAsync();

            return Ok(tipKorisnikas);
        }

        [HttpPost]
        public async Task<IActionResult> AddTipKorisnika([FromBody] TipKorisnika tipKorisnikaRequest)
        {

            await _applicationDbContext.TipKorisnikas.AddAsync(tipKorisnikaRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(tipKorisnikaRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTipKorisnika([FromRoute] int id)
        {
            var tipKorisnika = await _applicationDbContext.TipKorisnikas.FirstOrDefaultAsync(x => x.TipKorisnikaId == id);

            if (tipKorisnika == null)
                return NotFound();

            return Ok(tipKorisnika);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTipKorisnika([FromRoute] int id, TipKorisnika updateTipKorisnikaRequest)
        {
            var tipKorisnika = await _applicationDbContext.TipKorisnikas.FindAsync(id);

            if (tipKorisnika == null)
            {
                return NotFound();
            }

            tipKorisnika.Tip = updateTipKorisnikaRequest.Tip;
            tipKorisnika.Korisnik = updateTipKorisnikaRequest.Korisnik;


            await _applicationDbContext.SaveChangesAsync();
            return Ok(tipKorisnika);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTipKorisnika([FromRoute] int id)
        {
            var tipKorisnika = await _applicationDbContext.TipKorisnikas.FindAsync(id);


            if (tipKorisnika == null)
            {
                return NotFound();
            }

            _applicationDbContext.TipKorisnikas.Remove(tipKorisnika);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(tipKorisnika);
        }
    }
}
