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
    public class TipKorisnikaController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public TipKorisnikaController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public class TipKorisnikaAddVM
        {
            public string tipKorisnika { get; set; }
        }

        public class TipKorisnikaUpdateVM
        {
            public string tipKorisnika { get; set; }
        }

        [HttpGet]

        public List<TipKorisnika> GetAllTipKorisnikas()
        {
            var data = _applicationDbContext.TipKorisnikas
                .OrderBy(x => x.TipKorisnikaId).AsQueryable();

            return data.Take(100).ToList();
           
        }

        [HttpPost]
        public ActionResult<TipKorisnika> AddTipKorisnika([FromBody] TipKorisnikaAddVM x)
        {
            var newTip = new TipKorisnika
            {
                Tip=x.tipKorisnika,
            };

            _applicationDbContext.Add(newTip);
            _applicationDbContext.SaveChanges();

            return newTip;
           
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetTipKorisnika([FromRoute] int id)
        {
            var tipKorisnika = _applicationDbContext.TipKorisnikas.FirstOrDefault(x => x.TipKorisnikaId == id);

            if (tipKorisnika == null)
                return NotFound();

            return Ok(tipKorisnika);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateTipKorisnika(int id,[FromBody] TipKorisnikaUpdateVM x)
        {
            TipKorisnika tipKorisnika;

            if(id ==0 )
            {
                tipKorisnika = new TipKorisnika
                {
                    Tip = ""
                };
                _applicationDbContext.Add(tipKorisnika);
            }
            else
            {
                tipKorisnika = _applicationDbContext.TipKorisnikas.FirstOrDefault(x => x.TipKorisnikaId == id);
                if (tipKorisnika == null)
                    return BadRequest("Pogresan ID");
            }

            tipKorisnika.Tip = x.tipKorisnika;

            _applicationDbContext.SaveChanges();

            return Ok(tipKorisnika);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTipKorisnika(int id)
        {
            var tipKorisnika = _applicationDbContext.TipKorisnikas.Find(id);


            if (tipKorisnika == null)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(tipKorisnika);
            _applicationDbContext.SaveChanges();
            return Ok(tipKorisnika);
        }
    }
}
