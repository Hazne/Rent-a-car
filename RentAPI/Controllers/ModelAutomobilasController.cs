using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelAutomobilasController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public ModelAutomobilasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllModelAutomobilas()
        {
            var modelAutomobilas = await _applicationDbContext.ModelAutomobilas.ToListAsync();

            return Ok(modelAutomobilas);
        }

        [HttpPost]
        public async Task<IActionResult> AddModelAutomobila([FromBody] ModelAutomobila modelAutomobilaRequest)
        {

            await _applicationDbContext.ModelAutomobilas.AddAsync(modelAutomobilaRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(modelAutomobilaRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetModelAutomobila([FromRoute] int id)
        {
            var modelAutomobila = await _applicationDbContext.ModelAutomobilas.FirstOrDefaultAsync(x => x.ModelAutomobilaId == id);

            if (modelAutomobila == null)
                return NotFound();

            return Ok(modelAutomobila);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateModelAutomobila([FromRoute] int id, ModelAutomobila updateModelAutomobilaRequest)
        {
            var modelAutomobila = await _applicationDbContext.ModelAutomobilas.FindAsync(id);

            if (modelAutomobila == null)
            {
                return NotFound();
            }

            modelAutomobila.Opis = updateModelAutomobilaRequest.Opis;
            modelAutomobila.Automobil = updateModelAutomobilaRequest.Automobil;
            modelAutomobila.Proizvodjac = updateModelAutomobilaRequest.Proizvodjac;
            modelAutomobila.ImeModela = updateModelAutomobilaRequest.ImeModela;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(modelAutomobila);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteModelAutomobila([FromRoute] int id)
        {
            var modelAutomobila = await _applicationDbContext.ModelAutomobilas.FindAsync(id);


            if (modelAutomobila == null)
            {
                return NotFound();
            }

            _applicationDbContext.ModelAutomobilas.Remove(modelAutomobila);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(modelAutomobila);
        }
    }
}
