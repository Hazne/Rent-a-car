using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RentAPI.Helper;

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

        public ActionResult<List<Automobil>> GetAllAutomobils()
        {
            var data = _applicationDbContext.Automobils
                .Include(s => s.Izdavac)
                .Include(s => s.TipGoriva)
                .Include(s => s.TipAutomobila)
                .Include(s => s.ModelAutomobila)
                .OrderBy(x => x.AutomobilId).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<Automobil> AddAutomobil([FromBody] AutomobilVM x)
        {
            var newAutomobil = new Automobil
            {
                Tablice = x.Tablice,
                Slika = x.Slika,
                BrojAutomobila = x.BrojAutomobila,
                CijenaPoDanu = x.CijenaPoDanu,
                Opis = x.Opis,
                DatumProizvodnje = x.DatumProizvodnje,
                Kolometraza = x.Kolometraza,
                Vuca = x.Vuca,
                BrojSjedala = x.BrojSjedala,
                Status = x.Status,
                IzdavacId = x.IzdavacId,
                TipGorivaId = x.TipGorivaId,
                TipAutomobilaId = x.TipAutomobilaId,
                ModelAutomobilaId = x.ModelAutomobilaId
            };

            _applicationDbContext.Add(newAutomobil);
            _applicationDbContext.SaveChanges();

            return newAutomobil;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetAutomobil([FromRoute] int id)
        {
            var automobil = _applicationDbContext.Automobils
                .Include(s=>s.Izdavac)
                .Include(s=>s.TipGoriva)
                .Include(s=>s.TipAutomobila)
                .Include(s=>s.ModelAutomobila)
                .FirstOrDefault(x => x.AutomobilId == id);

            if (automobil == null)
                return BadRequest("Pogresan Id");

            return Ok(automobil);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateAutomobil([FromRoute] int id, AutomobilVM updateAutomobilRequest)
        {
            var automobil = _applicationDbContext.Automobils.Find(id);

            if (automobil == null)
            {
                return BadRequest("Pogresan Id");
            }

            automobil.BrojAutomobila = updateAutomobilRequest.BrojAutomobila;
            automobil.BrojSjedala = updateAutomobilRequest.BrojSjedala;
            automobil.CijenaPoDanu = updateAutomobilRequest.CijenaPoDanu;
            automobil.DatumProizvodnje = updateAutomobilRequest.DatumProizvodnje;
            automobil.Slika = updateAutomobilRequest.Slika;
            automobil.TipGorivaId = updateAutomobilRequest.TipGorivaId;
            automobil.Tablice = updateAutomobilRequest.Tablice;
            automobil.Vuca = updateAutomobilRequest.Vuca;
            automobil.Kolometraza = updateAutomobilRequest.Kolometraza;
            automobil.Opis = updateAutomobilRequest.Opis;
            automobil.Status = updateAutomobilRequest.Status;
            automobil.IzdavacId = updateAutomobilRequest.IzdavacId;
            automobil.ModelAutomobilaId = updateAutomobilRequest.ModelAutomobilaId;
           

            _applicationDbContext.SaveChanges();
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
