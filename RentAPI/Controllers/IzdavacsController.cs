using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IzdavacsController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public IzdavacsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllIzdavacs()
        {
            var izdavacs = await _applicationDbContext.Izdavacs.ToListAsync();

            return Ok(izdavacs);
        }

        [HttpPost]
        public async Task<IActionResult> AddIzdavac([FromBody] Izdavac izdavacRequest)
        {

            await _applicationDbContext.Izdavacs.AddAsync(izdavacRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(izdavacRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetIzdavac([FromRoute] int id)
        {
            var izdavac = await _applicationDbContext.Izdavacs.FirstOrDefaultAsync(x => x.IzdavacId == id);

            if (izdavac == null)
                return NotFound();

            return Ok(izdavac);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateIzdavac([FromRoute] int id, Izdavac updateIzdavacRequest)
        {
            var izdavac = await _applicationDbContext.Izdavacs.FindAsync(id);

            if (izdavac == null)
            {
                return NotFound();
            }

            izdavac.BrojMobitela = updateIzdavacRequest.BrojMobitela;
            izdavac.Adresa = updateIzdavacRequest.Adresa;
            izdavac.Automobil = updateIzdavacRequest.Automobil;
            izdavac.Grad = updateIzdavacRequest.Grad;
            izdavac.ImeIzdavaca = updateIzdavacRequest.ImeIzdavaca;
            izdavac.Opis = updateIzdavacRequest.Opis;
            izdavac.VrijemeOtvaranja = updateIzdavacRequest.VrijemeOtvaranja;
            izdavac.VrijemeZatvaranja = updateIzdavacRequest.VrijemeZatvaranja;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(izdavac);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteIzdavac([FromRoute] int id)
        {
            var izdavac = await _applicationDbContext.Izdavacs.FindAsync(id);


            if (izdavac == null)
            {
                return NotFound();
            }

            _applicationDbContext.Izdavacs.Remove(izdavac);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(izdavac);
        }
    }
}
