using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using RentAPI.Helper;
using RentAPI.Models;
using System;
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

        [HttpGet("{email}")]
        public ActionResult ForggotPassword(string email)
        {
            string novaSifra=HelperClass.NovaSifraGenerator();

            var korisnik = _applicationDbContext.Korisniks.FirstOrDefault(x=>x.Email== email);

            if(korisnik==null)
                return BadRequest("Ne postoji korisnik sa tim emailom");

            korisnik.Password = novaSifra;
            _applicationDbContext.SaveChanges();

            HelperClass.PosaljiMailNoviPassword(email, novaSifra);

            return Ok("Proslo");
        }

        [HttpGet("{email}")]
        public ActionResult Provjera(string email)
        {
            ProvjeraDatumaUTwoFac();

            var korisnik = _applicationDbContext.Korisniks.FirstOrDefault(x => x.Email == email);

            var provj = _applicationDbContext.TwoFactorsAuth.FirstOrDefault(x => x.KorisnikId == korisnik.KorisnikId);

            if (provj == null)
            {
                return Ok(false);
            }
            else if(provj.IsProvjerena == false)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        
        [HttpGet("{korisnikId}")]//not sure about HttpGet
        public ActionResult DodjelaTwoWayAuth(int korisnikId)
        {
            var korisnik = _applicationDbContext.Korisniks.FirstOrDefault(x=>x.KorisnikId== korisnikId);
            TwoFactorCodeModel auth = new TwoFactorCodeModel();

            string random = HelperClass.NovaSifraGenerator();

            auth.Token = random;
            auth.TrenutniDatum=DateTime.Now;
            auth.DatumIsteka= DateTime.Now.AddDays(5);
            auth.IsProvjerena = false;
            auth.KorisnikId= korisnikId;

            _applicationDbContext.TwoFactorsAuth.Add(auth);
            _applicationDbContext.SaveChanges();

            HelperClass.PosaljiMailTwoWayAuth(korisnik.Email, random);

            return Ok(auth);
        }

        [HttpGet("{korisnikId}")]
        public ActionResult ProvjeraValidnosti(int korisnikId, string provjera) 
        {
            var korisnik = _applicationDbContext.TwoFactorsAuth.FirstOrDefault(x=>x.KorisnikId == korisnikId);

            string kodToken = provjera;

            if(korisnik.Token.Equals(kodToken))
            {
                korisnik.IsProvjerena= true;
                _applicationDbContext.SaveChanges();
                return Ok(true);
            }

            return Ok(false);
        }

        private void ProvjeraDatumaUTwoFac()
        {
            var datumTrenutni = DateTime.Now;

            var datumZaObrisat = _applicationDbContext.TwoFactorsAuth.Where(x => x.DatumIsteka < datumTrenutni).ToList();

            foreach(var x in datumZaObrisat)
            {
                _applicationDbContext.TwoFactorsAuth.Remove(x);
            }
            _applicationDbContext.SaveChanges();
        }

    }
}
