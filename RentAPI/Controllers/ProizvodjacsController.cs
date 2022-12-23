using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        public class ProizvodjacAddVM
        {
            public string imeProizvodjaca { get; set; }
        }

        public class ProizvodjacUpdateVM
        {
            public string imeProizvodjaca { get; set; }
        }

        [HttpGet]

        public List<Proizvodjac> GetAllProizvodjacs()
        {
            var data = _applicationDbContext.Proizvodjacs
                .OrderBy(x => x.ProizvodjacId).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<Proizvodjac> AddProizvodjac([FromBody] ProizvodjacAddVM x)
        {
            var proizvod = new Proizvodjac
            {
                ImeProizvodjaca = x.imeProizvodjaca
            };

            _applicationDbContext.Add(proizvod);
            _applicationDbContext.SaveChanges();

            return proizvod;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetProizvodjac([FromRoute] int id)
        {
            var proizvodjac = _applicationDbContext.Proizvodjacs.FirstOrDefault(x => x.ProizvodjacId == id);

            if (proizvodjac == null)
                return NotFound();

            return Ok(proizvodjac);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateProizvodjac([FromRoute] int id, ProizvodjacUpdateVM x)
        {
            Proizvodjac proizvodjac;

            if(id == 0)
            {
                proizvodjac = new Proizvodjac
                {
                    ImeProizvodjaca = ""
                };
                _applicationDbContext.Add(proizvodjac);
            }
            else
            {
                proizvodjac = _applicationDbContext.Proizvodjacs.FirstOrDefault(x => x.ProizvodjacId == id);
                if (proizvodjac == null)
                    return BadRequest("Pogresan ID");
            }

            proizvodjac.ImeProizvodjaca=x.imeProizvodjaca;

            _applicationDbContext.SaveChanges();
            return GetProizvodjac(proizvodjac.ProizvodjacId);
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
