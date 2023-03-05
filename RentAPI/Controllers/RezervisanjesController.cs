using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using RentAPI.Data;
using RentAPI.Helper;
using RentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervisanjesController : Controller
    {

        public readonly ApplicationDbContext _applicationDbContext;

        public RezervisanjesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        [HttpGet("{id}")]
        public ActionResult GetProvjeraDatuma(int id)
        {
            var rezervacija = _applicationDbContext.Rezervisanjes.Where(x => x.AutomobilId == id).OrderByDescending(x => x.DatumZavrsetka).FirstOrDefault();

            return Ok(rezervacija);
        }


        [HttpGet]

        public ActionResult<List<Rezervisanje>> GetAllRezervisanjes()
        {
            var rezervisanjes = _applicationDbContext.Rezervisanjes
                //.Include(s => s.Korisnik)
                //.Include(s => s.Automobil)
                .OrderBy(s => s.RezervisanjeId).AsQueryable();

            return rezervisanjes.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<Rezervisanje> AddRezervisanje([FromBody] RezervisanjeVM x)
        {

            var korisnik = _applicationDbContext.Korisniks.Find(x.KorisnikId);

            if (korisnik == null)
            {
                return BadRequest("Korisnik ne postoji");
            }

            var nova = new Rezervisanje();
            _applicationDbContext.Add(nova);

            nova.DatumPocetka = x.DatumPocetka;
            nova.DatumZavrsetka = x.DatumZavrsetka;
            nova.KorisnikId = x.KorisnikId;
            nova.AutomobilId = x.AutomobilId;

            PosaljiMail(korisnik.Email);

            _applicationDbContext.SaveChanges();
            return Ok(nova);
        }

        public static void PosaljiMail(string email)
        {

            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials= false;

            client.Credentials = new System.Net.NetworkCredential("automobili2139@gmail.com", "kgevyjbdlrvrqdmj");

            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress("automobili2139@gmail.com");

            mailMessage.To.Add(new MailAddress(email));

            mailMessage.Body = "<html><body> " +
                "<h3>Test Body</h3>" +
                "<a href="+"facebook.com"+">Tekst</a>" +
                " </body></html>";
            mailMessage.Subject = "REZERVACIJA AUTOMOBILA. " + DateTime.Now;
            mailMessage.IsBodyHtml= true;

            mailMessage.BodyEncoding=System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding=System.Text.Encoding.UTF8;

            client.SendMailAsync(mailMessage);

        }
     

        [HttpGet("{korisnikId}")]
        public ActionResult GetRezervisanje(int korisnikId)
        {
            var korisnik = _applicationDbContext.Korisniks.Find(korisnikId);

            if (korisnik == null)
                return BadRequest("Ne postoji korisnik sa tim ID-om");

            var rezervisanje = _applicationDbContext.Rezervisanjes.Include(x => x.Automobil.ModelAutomobila)
                                                                   .Where(x => x.KorisnikId == korisnikId && x.DatumZavrsetka >= DateTime.Now);

            foreach(var rezervisan in rezervisanje)
            {
                rezervisan.Korisnik = null;
            }

            return Ok(rezervisanje);
        }

        [HttpGet("{korisnikId}")]
        public ActionResult GetZavrseneRezervacije(int korisnikId)
        {
            var korisnik = _applicationDbContext.Korisniks.Find(korisnikId);

            if (korisnik == null)
                return BadRequest("Ne postoji korisnik sa tim ID-om");

            var rezervisanje = _applicationDbContext.Rezervisanjes.Include(x => x.Automobil.ModelAutomobila)
                                                                   .Where(x => x.KorisnikId == korisnikId && x.DatumZavrsetka <= DateTime.Now).OrderByDescending(x=>x.DatumZavrsetka);

            foreach (var rezervisan in rezervisanje)
            {
                rezervisan.Korisnik = null;
            }

            return Ok(rezervisanje);
        }


        [HttpPost("{id}")]
        public ActionResult UpdateRezervisanje(int id,[FromBody] Rezervisanje updateRezervisanjeRequest)
        {
            Rezervisanje rezervisanje;

            if (id == 0)
            {
                rezervisanje = new Rezervisanje();
                _applicationDbContext.Add(rezervisanje);
            }
            else
            {
                rezervisanje = _applicationDbContext.Rezervisanjes.Find(id);
                if (rezervisanje == null)
                {
                    return BadRequest("Ne postoji ID");
                }
            }

            rezervisanje.StatusOcjene = updateRezervisanjeRequest.StatusOcjene;
            rezervisanje.StatusKomentara = updateRezervisanjeRequest.StatusKomentara;
            rezervisanje.Automobil = updateRezervisanjeRequest.Automobil;
            rezervisanje.AutomobilId = updateRezervisanjeRequest.AutomobilId;
            rezervisanje.DatumPocetka = updateRezervisanjeRequest.DatumPocetka;
            rezervisanje.DatumZavrsetka = updateRezervisanjeRequest.DatumZavrsetka;
            rezervisanje.Korisnik = updateRezervisanjeRequest.Korisnik;
            rezervisanje.KorisnikId = updateRezervisanjeRequest.KorisnikId;

            _applicationDbContext.SaveChanges();
            return Ok(rezervisanje);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRezervisanje( int id)
        {
            var rezervisanje = _applicationDbContext.Rezervisanjes.Find(id);


            if (rezervisanje == null)
            {
                return NotFound();
            }

            _applicationDbContext.Rezervisanjes.Remove(rezervisanje);
            _applicationDbContext.SaveChanges();
            return Ok(rezervisanje);
        }
    }
}
