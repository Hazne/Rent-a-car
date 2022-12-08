using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipGorivasController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public TipGorivasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllTipGorivas()
        {
            var tipGorivas = await _applicationDbContext.TipGorivas.ToListAsync();

            return Ok(tipGorivas);
        }

        [HttpPost]
        public async Task<IActionResult> AddTipGoriva([FromBody] TipGoriva tipGorivaRequest)
        {

            await _applicationDbContext.TipGorivas.AddAsync(tipGorivaRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(tipGorivaRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTipGoriva([FromRoute] int id)
        {
            var tipGoriva = await _applicationDbContext.TipGorivas.FirstOrDefaultAsync(x => x.TipGorivaId == id);

            if (tipGoriva == null)
                return NotFound();

            return Ok(tipGoriva);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTipGoriva([FromRoute] int id, TipGoriva updateTipGorivaRequest)
        {
            var tipGoriva = await _applicationDbContext.TipGorivas.FindAsync(id);

            if (tipGoriva == null)
            {
                return NotFound();
            }

            tipGoriva.ImeGoriva = updateTipGorivaRequest.ImeGoriva;
            tipGoriva.Automobil = updateTipGorivaRequest.Automobil;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(tipGoriva);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTipGoriva([FromRoute] int id)
        {
            var tipGoriva = await _applicationDbContext.TipGorivas.FindAsync(id);


            if (tipGoriva == null)
            {
                return NotFound();
            }

            _applicationDbContext.TipGorivas.Remove(tipGoriva);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(tipGoriva);
        }
    }
}
