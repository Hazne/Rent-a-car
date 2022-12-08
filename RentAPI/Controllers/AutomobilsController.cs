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
    public class AutomobilsController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public AutomobilsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllAutomobils()
        {
            var automobils = await _applicationDbContext.Automobils.ToListAsync();

            return Ok(automobils);
        }

        [HttpPost]
        public async Task<IActionResult> AddAutomobil([FromBody] Automobil automobilRequest)
        {

            await _applicationDbContext.Automobils.AddAsync(automobilRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(automobilRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAutomobil([FromRoute] int id)
        {
            var automobil = await _applicationDbContext.Automobils.FirstOrDefaultAsync(x => x.AutomobilId == id);

            if (automobil == null)
                return NotFound();

            return Ok(automobil);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAutomobil([FromRoute] int id, Automobil updateAutomobilRequest)
        {
            var automobil = await _applicationDbContext.Automobils.FindAsync(id);

            if (automobil == null)
            {
                return NotFound();
            }

            automobil.BrojAutomobila = updateAutomobilRequest.BrojAutomobila;
            automobil.BrojSjedala = updateAutomobilRequest.BrojSjedala;
            automobil.CijenaPoDanu = updateAutomobilRequest.CijenaPoDanu;
            automobil.DatumProizvodnje = updateAutomobilRequest.DatumProizvodnje;
            automobil.Slika = updateAutomobilRequest.Slika;
            automobil.TipGoriva = updateAutomobilRequest.TipGoriva;
            automobil.Tablice = updateAutomobilRequest.Tablice;
            automobil.Vuca = updateAutomobilRequest.Vuca;
            automobil.Kolometraza = updateAutomobilRequest.Kolometraza;
            automobil.Opis = updateAutomobilRequest.Opis;
            automobil.Status = updateAutomobilRequest.Status;
            automobil.Izdavac = updateAutomobilRequest.Izdavac;
            automobil.Ocjena = updateAutomobilRequest.Ocjena;
            automobil.Komentar = updateAutomobilRequest.Komentar;
            automobil.ModelAutomobila = updateAutomobilRequest.ModelAutomobila;
            automobil.Rezervisanje = updateAutomobilRequest.Rezervisanje;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(automobil);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAutomobil([FromRoute] int id)
        {
            var automobil = await _applicationDbContext.Automobils.FindAsync(id);


            if (automobil == null)
            {
                return NotFound();
            }

            _applicationDbContext.Automobils.Remove(automobil);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(automobil);
        }
    }
}
