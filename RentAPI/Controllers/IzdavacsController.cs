using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using RentAPI.Helper;

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

        public ActionResult<List<Izdavac>> GetAllIzdavacs()
        {
            var data = _applicationDbContext.Izdavacs
                .Include(s => s.Grad)
                .OrderBy(s => s.IzdavacId).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<Izdavac> AddIzdavac([FromBody] IzdavacVM x)
        {
            var newIzdavac = new Izdavac
            {
                ImeIzdavaca = x.ImeIzdavaca,
                BrojMobitela = x.BrojMobitela,
                Adresa = x.Adresa,
                VrijemeOtvaranja = x.VrijemeOtvaranja,
                VrijemeZatvaranja = x.VrijemeZatvaranja,
                Opis = x.Opis,
                GradId = x.GradId,
            };

            _applicationDbContext.Add(newIzdavac);
            _applicationDbContext.SaveChanges();

            return newIzdavac;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetIzdavac([FromRoute] int id)
        {
            var izdavac = _applicationDbContext.Izdavacs.Include(x=>x.Grad).FirstOrDefault(x => x.IzdavacId == id);

            if (izdavac == null)
                return BadRequest("Pogresan ID");

            return Ok(izdavac);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateIzdavac(int id,[FromBody] IzdavacVM x)
        {
            Izdavac izdavac;

            if (id == 0)
            {
                izdavac = new Izdavac();
                _applicationDbContext.Add(izdavac);
            }
            else
            {
                izdavac = _applicationDbContext.Izdavacs.Find(id);
                if (izdavac == null)
                {
                    return BadRequest("Ne posotoji taj Izdavac Id");
                }
            }

            izdavac.BrojMobitela = x.BrojMobitela;
            izdavac.Adresa = x.Adresa;
            izdavac.GradId = x.GradId;
            izdavac.ImeIzdavaca = x.ImeIzdavaca;
            izdavac.Opis = x.Opis;
            izdavac.VrijemeOtvaranja = x.VrijemeOtvaranja;
            izdavac.VrijemeZatvaranja = x.VrijemeZatvaranja;

            _applicationDbContext.SaveChanges();
            return Ok(izdavac);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteIzdavac(int id)
        {
            var izdavac =  _applicationDbContext.Izdavacs.Find(id);


            if (izdavac == null)
            {
                return BadRequest("Pogresan ID");

            }

            _applicationDbContext.Remove(izdavac);
            _applicationDbContext.SaveChanges();
            return Ok(izdavac);
        }
    }
}
