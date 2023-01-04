using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAPI.Helper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class KorisniksController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _config;

        public KorisniksController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

       
        [HttpGet]

        public ActionResult<List<Korisnik>> GetAllKorisniks()
        {
            var data = _applicationDbContext.Korisniks
                .Include(s => s.Grad)
                .Include(x => x.TipKorisnika)
                .OrderBy(x => x.KorisnikId)
                .AsQueryable();

            return data.Take(100).ToList();
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult Create(Register user)
        {
            if (_applicationDbContext.Korisniks.Where(x => x.Email == user.Email).FirstOrDefault() != null)
            {
                return Ok("AlreadyExist");
            }
            if (_applicationDbContext.Korisniks.Where(u => u.Username == user.Username).FirstOrDefault() != null)
            {
                return Ok("AlredyExistUser");
            }
            //var postojeciGrad ="";
            Grad postojeciGrad;

            if (_applicationDbContext.Grads.Where(x => x.ImeGrada.ToLower() == user.Grad.ToLower()).FirstOrDefault() != null)
            {
                postojeciGrad = _applicationDbContext.Grads.Where(x => x.ImeGrada.ToLower() == user.Grad.ToLower()).FirstOrDefault(x => x.ImeGrada == user.Grad);

                //return Ok("asdsa");
            }
            else
            {
                postojeciGrad = new Grad()
                {
                    ImeGrada = user.Grad.ToLower()
                };
            }

            //var korisnik = _applicationDbContext.Korisniks.FirstOrDefault(x=> x.Username == user.Username);


            //korisnik.Token = CreateJwt(korisnik);

            var noviKorisnik = new Korisnik
            {
                Ime = user.Ime,
                Prezime = user.Prezime,
                Email = user.Email,
                BrojMobitela = user.BrojMobitela,
                Username = user.Username,
                Password = user.Password,
                Grad = postojeciGrad,
                TipKorisnika = _applicationDbContext.TipKorisnikas.Where(x => x.TipKorisnikaId == 1).First(),
            };


            _applicationDbContext.Add(noviKorisnik);
            _applicationDbContext.SaveChanges();
            return Ok("Success");
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetKorisnik([FromRoute] int id)
        {
            var korisnik = _applicationDbContext.Korisniks
                .Include(x => x.TipKorisnika)
                .Include(x => x.Grad)
                .FirstOrDefault(x => x.KorisnikId == id);

            if (korisnik == null)
                return BadRequest("Pogresan ID");

            return Ok(korisnik);
        }

        
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromBody]Login userObj)
        {
        
            if (userObj == null)
                return BadRequest();

            var user = await _applicationDbContext.Korisniks
                .FirstOrDefaultAsync(x=>x.Email == userObj.Email && userObj.Pwd == x.Password);
            if (user == null)
                return NotFound(new { Message = "User Not found!" });


            user.Token = CreateJwt(user);

            return Ok(new
            {
                Token = user.Token,
                Message = "Uspjesno"
            });

        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateKorisnik([FromRoute] int id, KorisnikVM x)
        {
            var korisnik = _applicationDbContext.Korisniks.Find(id);

            if (korisnik == null)
            {
                return BadRequest("Pogresan ID");

            }

            korisnik.Ime = x.ime;
            korisnik.Prezime = x.prezime;
            korisnik.Email = x.email;
            korisnik.BrojMobitela = x.brojMobitela;
            korisnik.Username = x.username;
            korisnik.GradId = x.gradId;
            korisnik.TipKorisnikaId = x.tipKorisnikaId;



            _applicationDbContext.SaveChanges();
            return Ok(korisnik);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteKorisnik([FromRoute] int id)
        {
            var korisnik = await _applicationDbContext.Korisniks.FindAsync(id);


            if (korisnik == null)
            {
                return NotFound();
            }

            _applicationDbContext.Korisniks.Remove(korisnik);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(korisnik);
        }

        private string CreateJwt(Korisnik korisnik)
        {
 
            var tipKorisnika = _applicationDbContext.TipKorisnikas.Where(x=>x.TipKorisnikaId == korisnik.TipKorisnikaId).FirstOrDefault();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, tipKorisnika.Tip.ToString()),
                new Claim(ClaimTypes.Name, $"{korisnik.Ime} {korisnik.Prezime}")
            });

            var credientials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credientials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
