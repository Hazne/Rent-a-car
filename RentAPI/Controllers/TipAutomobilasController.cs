using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
using System.Collections.Generic;
using System.Linq;
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
            this._applicationDbContext = applicationDbContext;
        }

        public class TipAutomoboilaAddVM
        {
            public string ImeTipa { get; set; }
        }
        
        public class TipAutomobilaUpdateVM
        {
            public string ImeTipa { get; set; }
        }

        [HttpGet]

        public List<TipAutomobila> GetAllTipAutomobilas()
        {
            var data = _applicationDbContext.TipAutomobilas
                .OrderBy(x => x.TipAutomobilaId).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<TipAutomobila> AddTipAutomobila([FromBody] TipAutomoboilaAddVM x)
        {
            var newTipAutomobila = new TipAutomobila
            {
                ImeTipa = x.ImeTipa,
            };

            _applicationDbContext.Add(newTipAutomobila);
            _applicationDbContext.SaveChanges();

            return newTipAutomobila;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetTipAutomobila([FromRoute] int id)
        {
            var tipAutomobila = _applicationDbContext.TipAutomobilas.FirstOrDefault(x => x.TipAutomobilaId == id);

            if (tipAutomobila == null)
                return NotFound();

            return Ok(tipAutomobila);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateTipAutomobila(int id, [FromBody] TipAutomobilaUpdateVM x)
        {
            TipAutomobila tipAutomobila;

            if (id == 0)
            {
                tipAutomobila = new TipAutomobila
                {
                    ImeTipa = ""
                };
                _applicationDbContext.Add(tipAutomobila);
            }
            else
            {
                tipAutomobila= _applicationDbContext.TipAutomobilas.FirstOrDefault(x=>x.TipAutomobilaId==id);
                if (tipAutomobila == null)
                    return BadRequest("Pogresan ID");
            }

            tipAutomobila.ImeTipa = x.ImeTipa;

            _applicationDbContext.SaveChanges();
            return Ok(tipAutomobila);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTipAutomobila(int id)
        {
            var tipAutomobila = _applicationDbContext.TipAutomobilas.Find(id);


            if (tipAutomobila == null)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(tipAutomobila);
            _applicationDbContext.SaveChangesAsync();
            return Ok(tipAutomobila);
        }
    }
}
