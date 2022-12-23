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

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateIzdavac([FromRoute] int id, IzdavacVM x)
        {
            var izdavac = _applicationDbContext.Izdavacs.Find(id);

            if (izdavac == null)
            {
                return BadRequest("Pogresan ID");

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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteIzdavac([FromRoute] int id)
        {
            var izdavac = await _applicationDbContext.Izdavacs.FindAsync(id);


            if (izdavac == null)
            {
                return BadRequest("Pogresan ID");

            }

            _applicationDbContext.Izdavacs.Remove(izdavac);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(izdavac);
        }
    }
}
