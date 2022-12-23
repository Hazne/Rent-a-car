using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipGorivasController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public TipGorivasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public class TipGorivaAddVM
        {
            public string ImeGoriva { get; set; }
        }

        public class TipGorivaUpdateVM
        {
            public string ImeGoriva { get; set; }
        }
      
        [HttpGet]

        public List<TipGoriva> GetAllTipGorivas()
        {
            var data = _applicationDbContext.TipGorivas
                .OrderBy(x => x.TipGorivaId).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<TipGoriva> AddTipGoriva([FromBody] TipGorivaAddVM x)
        {
            var newTipGoriva = new TipGoriva
            {
                ImeGoriva = x.ImeGoriva
            };

            _applicationDbContext.Add(newTipGoriva);
            _applicationDbContext.SaveChanges();

            return newTipGoriva;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetTipGoriva([FromRoute] int id)
        {
            var tipGoriva = _applicationDbContext.TipGorivas.FirstOrDefault(x => x.TipGorivaId == id);

            if (tipGoriva == null)
                return NotFound();

            return Ok(tipGoriva);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateTipGoriva([FromRoute] int id, TipGorivaUpdateVM x)
        {
            TipGoriva tipGoriva;

            if (id == 0)
            {
                tipGoriva = new TipGoriva
                {
                    ImeGoriva = ""
                };
                _applicationDbContext.Add(tipGoriva);
            }
            else
            {
                tipGoriva = _applicationDbContext.TipGorivas.FirstOrDefault(x => x.TipGorivaId == id);
                if (tipGoriva == null)
                    return BadRequest("Pogresan ID");
            }

            tipGoriva.ImeGoriva = x.ImeGoriva;

            _applicationDbContext.SaveChanges();
            return GetTipGoriva(tipGoriva.TipGorivaId);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTipGoriva([FromRoute] int id)
        {
            var tipGoriva = await _applicationDbContext.TipGorivas.FindAsync(id);


            if (tipGoriva == null)
            {
                return NotFound();
            }

            _applicationDbContext.TipGorivas.Remove(tipGoriva);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(tipGoriva);
        }
    }
}
