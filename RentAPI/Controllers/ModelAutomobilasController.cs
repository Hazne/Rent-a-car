using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Helper;
using RentAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public ActionResult<List<ModelAutomobila>> GetAllModelAutomobilas()
        {
            var data = _applicationDbContext.ModelAutomobilas
                .Include(s => s.Proizvodjac)
                 .OrderBy(x => x.ModelAutomobilaId)
                 .AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<ModelAutomobila> AddModelAutomobila([FromBody] ModelAutomobilaVM x)
        {
            var newModelAutomobila = new ModelAutomobila
            {
                ImeModela = x.imeModela,
                Opis = x.opis,
                ProizvodjacId = x.proizvodjacId
            };

            _applicationDbContext.Add(newModelAutomobila);
            _applicationDbContext.SaveChanges();

            return newModelAutomobila;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetModelAutomobila([FromRoute] int id)
        {
            var modelAutomobila = _applicationDbContext.ModelAutomobilas.Include(x=>x.Proizvodjac).FirstOrDefault(x => x.ModelAutomobilaId == id);

            if (modelAutomobila == null)
                return NotFound();
            
            return Ok(modelAutomobila);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateModelAutomobila([FromRoute] int id, ModelAutomobilaVM x)
        {
            ModelAutomobila modelAutomobila;

            if(id == 0 )
            {
                modelAutomobila = new ModelAutomobila
                {
                    ImeModela = "",
                    Opis = "",
                    ProizvodjacId = id
                };
                _applicationDbContext.Add(modelAutomobila);
            }
            else
            {
                modelAutomobila=_applicationDbContext.ModelAutomobilas.Include(s=>s.Proizvodjac).FirstOrDefault(s=>s.ModelAutomobilaId==id);
                if (modelAutomobila == null)
                    return BadRequest("Pogresan ID");
            }

            modelAutomobila.ImeModela= x.imeModela;
            modelAutomobila.Opis= x.opis;
            modelAutomobila.ProizvodjacId= x.proizvodjacId;

            _applicationDbContext.SaveChanges();
            return GetModelAutomobila(modelAutomobila.ModelAutomobilaId);
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
