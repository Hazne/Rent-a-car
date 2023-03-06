using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Linq;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors("AllowOrigin")]

    public class LoginHellperController : ControllerBase
    {
        public readonly ApplicationDbContext _applicationDbContext;

        public LoginHellperController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext= applicationDbContext;
        }

        [HttpGet("{email}")]
        public ActionResult ProvjeraEmaila(string email)
        {
            var korisnik = _applicationDbContext.Korisniks.FirstOrDefault(x => x.Email == email);

            if (korisnik == null)
                return Ok(false); 
            else
            {
                return Ok(true);
            }
        }
    }
}
