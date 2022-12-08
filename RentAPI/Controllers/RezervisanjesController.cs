using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RezervisanjesController : Controller
    {
        
        public readonly ApplicationDbContext _applicationDbContext;
        public RezervisanjesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllRezervisanjes()
        {
            var rezervisanjes = await _applicationDbContext.Rezervisanjes.ToListAsync();

            return Ok(rezervisanjes);
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRezervisanje([FromRoute] int id, Rezervisanje updateRezervisanjeRequest)
        {
            var rezervisanje = await _applicationDbContext.Rezervisanjes.FindAsync(id);

            if (rezervisanje == null)
            {
                return NotFound();
            }

            rezervisanje.StatusOcjene = updateRezervisanjeRequest.StatusOcjene;
            rezervisanje.StatusKomentara = updateRezervisanjeRequest.StatusKomentara;
            rezervisanje.Automobil = updateRezervisanjeRequest.Automobil;
            rezervisanje.DatumPocetka = updateRezervisanjeRequest.DatumPocetka;
            rezervisanje.DatumZavrsetka = updateRezervisanjeRequest.DatumZavrsetka;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(rezervisanje);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRezervisanje([FromRoute] int id)
        {
            var rezervisanje = await _applicationDbContext.Rezervisanjes.FindAsync(id);


            if (rezervisanje == null)
            {
                return NotFound();
            }

            _applicationDbContext.Rezervisanjes.Remove(rezervisanje);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(rezervisanje);
        }
    }
}
