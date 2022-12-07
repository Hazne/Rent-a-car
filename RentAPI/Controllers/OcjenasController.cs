using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;
using System;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcjenasController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public OcjenasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOcjene()
        {
            var ocjena = await _applicationDbContext.Ocjenas.ToListAsync();

            return Ok(ocjena);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOcjena([FromRoute] int id)
        {
            var ocjena = await _applicationDbContext.Ocjenas.FirstOrDefaultAsync(x => x.OcjenaId == id);

            if (ocjena == null)
                return NotFound();

            return Ok(ocjena);
        }

        [HttpPost]
        public async Task<IActionResult> AddOcjena([FromBody] Ocjena ocjenaRequest)
        {

            await _applicationDbContext.Ocjenas.AddAsync(ocjenaRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(ocjenaRequest);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOcjena([FromRoute] int id, Ocjena updateOcjenaRequest)
        {
            var ocjena = await _applicationDbContext.Ocjenas.FindAsync(id);

            if (ocjena == null)
            {
                return NotFound();
            }

            ocjena.BrojOcjene = updateOcjenaRequest.BrojOcjene;
            ocjena.DatumOcjene = updateOcjenaRequest.DatumOcjene;
            ocjena.Automobil = updateOcjenaRequest.Automobil;
            

            await _applicationDbContext.SaveChangesAsync();
            return Ok(ocjena);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOcjena([FromRoute] Guid id)
        {
            var ocjena = await _applicationDbContext.Ocjenas.FindAsync(id);


            if (ocjena == null)
            {
                return NotFound();
            }

            _applicationDbContext.Ocjenas.Remove(ocjena);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(ocjena);
        }

        
    }
}
