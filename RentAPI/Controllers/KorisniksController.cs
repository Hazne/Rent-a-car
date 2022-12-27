﻿using Microsoft.AspNetCore.Mvc;
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

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class KorisniksController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public KorisniksController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public ActionResult<List<Korisnik>> GetAllKorisniks()
        {
            var data = _applicationDbContext.Korisniks
                .Include(s=>s.Grad)
                .Include(x=>x.TipKorisnika)
                .OrderBy(x=>x.KorisnikId)
                .AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost("CreateUser")]
        public IActionResult Create(Register user)
        {
            if(_applicationDbContext.Korisniks.Where(x=>x.Email == user.Email).FirstOrDefault() != null)
            {
                return Ok("AlreadyExist");
            }
            if(_applicationDbContext.Korisniks.Where(u=>u.Username == user.Username).FirstOrDefault() != null)
            {
                return Ok("AlredyExistUser");
            }
            //var postojeciGrad ="";
            Grad postojeciGrad;

            if (_applicationDbContext.Grads.Where(x => x.ImeGrada.ToLower() == user.Grad.ToLower()).FirstOrDefault() != null)
            {
                postojeciGrad=_applicationDbContext.Grads.Where(x=>x.ImeGrada.ToLower() == user.Grad.ToLower()).FirstOrDefault(x=>x.ImeGrada == user.Grad);

                //return Ok("asdsa");
            }
            else
            {
                postojeciGrad = new Grad()
                {
                    ImeGrada = user.Grad.ToLower()
                };
            }

            

            var noviKorisnik = new Korisnik
            {
                Ime = user.Ime,
                Prezime = user.Prezime,
                Email = user.Email,
                BrojMobitela = user.BrojMobitela,
                Username = user.Username,
                Password = user.Password,
                Grad = postojeciGrad,
                TipKorisnika=_applicationDbContext.TipKorisnikas.Where(x=>x.TipKorisnikaId == 1).First()
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
                .Include(x=>x.TipKorisnika)
                .Include(x=>x.Grad)
                .FirstOrDefault(x => x.KorisnikId == id);

            if (korisnik == null)
                return BadRequest("Pogresan ID");

            return Ok(korisnik);
        }

        [HttpPost ("LoginUser")]
        public IActionResult Login(Login user)
        {
            var korisnikPostoji = _applicationDbContext.Korisniks.Where(x => x.Email == user.Email && x.Password == user.Pwd).FirstOrDefault();

            if(korisnikPostoji != null)
            {
                return Ok("Uspjesno");
            }

            return Ok("Neuspjesno");
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
    }
}
