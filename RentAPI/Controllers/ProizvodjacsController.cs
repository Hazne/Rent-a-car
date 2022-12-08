using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodjacsController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public ProizvodjacsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllProizvodjacs()
        {
            var proizvodjacs = await _applicationDbContext.Proizvodjacs.ToListAsync();

            return Ok(proizvodjacs);
        }

        [HttpPost]
        public async Task<IActionResult> AddProizvodjac([FromBody] Proizvodjac proizvodjacRequest)
        {

            await _applicationDbContext.Proizvodjacs.AddAsync(proizvodjacRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(proizvodjacRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProizvodjac([FromRoute] int id)
        {
            var proizvodjac = await _applicationDbContext.Proizvodjacs.FirstOrDefaultAsync(x => x.ProizvodjacId == id);

            if (proizvodjac == null)
                return NotFound();

            return Ok(proizvodjac);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProizvodjac([FromRoute] int id, Proizvodjac updateProizvodjacRequest)
        {
            var proizvodjac = await _applicationDbContext.Proizvodjacs.FindAsync(id);

            if (proizvodjac == null)
            {
                return NotFound();
            }

            proizvodjac.ImeProizvodjaca = updateProizvodjacRequest.ImeProizvodjaca;
            proizvodjac.ModelAutomobila = updateProizvodjacRequest.ModelAutomobila;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(proizvodjac);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProizvodjac([FromRoute] int id)
        {
            var proizvodjac = await _applicationDbContext.Proizvodjacs.FindAsync(id);


            if (proizvodjac == null)
            {
                return NotFound();
            }

            _applicationDbContext.Proizvodjacs.Remove(proizvodjac);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(proizvodjac);
        }
    }
}
