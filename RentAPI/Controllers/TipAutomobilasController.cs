using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipAutomobilasController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public TipAutomobilasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllTipAutomobilas()
        {
            var tipAutomobilas = await _applicationDbContext.TipAutomobilas.ToListAsync();

            return Ok(tipAutomobilas);
        }

        [HttpPost]
        public async Task<IActionResult> AddTipAutomobila([FromBody] TipAutomobila tipAutomobilaRequest)
        {

            await _applicationDbContext.TipAutomobilas.AddAsync(tipAutomobilaRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(tipAutomobilaRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTipAutomobila([FromRoute] int id)
        {
            var tipAutomobila = await _applicationDbContext.TipAutomobilas.FirstOrDefaultAsync(x => x.TipAutomobilaId == id);

            if (tipAutomobila == null)
                return NotFound();

            return Ok(tipAutomobila);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTipAutomobila([FromRoute] int id, TipAutomobila updateTipAutomobilaRequest)
        {
            var tipAutomobila = await _applicationDbContext.TipAutomobilas.FindAsync(id);

            if (tipAutomobila == null)
            {
                return NotFound();
            }
            
            tipAutomobila.ImeTipa = updateTipAutomobilaRequest.ImeTipa;
            tipAutomobila.Automobil = updateTipAutomobilaRequest.Automobil;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(tipAutomobila);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTipAutomobila([FromRoute] int id)
        {
            var tipAutomobila = await _applicationDbContext.TipAutomobilas.FindAsync(id);


            if (tipAutomobila == null)
            {
                return NotFound();
            }

            _applicationDbContext.TipAutomobilas.Remove(tipAutomobila);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(tipAutomobila);
        }
    }
}
