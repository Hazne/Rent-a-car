﻿using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RentAPI.Helper;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OcjenasController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public OcjenasController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public ActionResult<List<Ocjena>> GetAllOcjene()
        {
            var data = _applicationDbContext.Ocjenas
                .OrderBy(x => x.OcjenaId).AsQueryable();

            return data.Take(100).ToList();
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOcjena([FromRoute] int id)
        {
            var ocjena = _applicationDbContext.Ocjenas
                .Include(x => x.Korisnik)
                .Include(x => x.Automobil)
                .FirstOrDefault(x => x.OcjenaId == id);

            if (ocjena == null)
                return BadRequest("Pogresan ID");

            return Ok(ocjena);
        }

        [HttpPost]
        public ActionResult<Ocjena> AddOcjena([FromBody] OcjenaVM x)
        {
            var newOcjena = new Ocjena
            {
                BrojOcjene = x.BrojOcjene,
                DatumOcjene = x.DatumOcjene,
                KorisnikId = x.KorisnikId,
                AutomobilId = x.AutomobilId,
                RezervisanjeId = x.RezervisanjeId
            };

            StatusOcjene(newOcjena.RezervisanjeId);
            _applicationDbContext.Add(newOcjena);
            _applicationDbContext.SaveChanges();

            return newOcjena;
        }

        private void StatusOcjene(int rezervisanjeId)
        {
            var rezervacija = _applicationDbContext.Rezervisanjes.Find(rezervisanjeId);

            rezervacija.StatusOcjene = true;

        }

        [HttpPost("{id}")]
        public ActionResult UpdateOcjena(int id,[FromBody] Ocjena x)
        {
            Ocjena ocjena;

            if (id == 0)
            {
                ocjena = new Ocjena
                {
                    DatumOcjene = DateTime.Now
                };
                _applicationDbContext.Add(ocjena);
            }
            else
            {
                ocjena = _applicationDbContext.Ocjenas.FirstOrDefault(x => x.OcjenaId == id);
                if (ocjena == null)
                {
                    return BadRequest("Pogresan ID");
                }
            }

            ocjena.BrojOcjene = x.BrojOcjene;
            ocjena.DatumOcjene = x.DatumOcjene;
            ocjena.AutomobilId = x.AutomobilId;
            ocjena.Automobil = x.Automobil;
            ocjena.KorisnikId = x.KorisnikId;
            ocjena.Korisnik = x.Korisnik;
            ocjena.RezervisanjeId = x.RezervisanjeId;

            var korisnik = _applicationDbContext.Korisniks.FirstOrDefault(k => k.KorisnikId == x.KorisnikId);
            if (korisnik != null)
            {
                ocjena.Korisnik = korisnik;
            }
            else
            {
                // Return an error message if the "Korisnik" object does not exist
                return BadRequest("Korisnik ne postoji");
            }

            StatusOcjene(ocjena.RezervisanjeId);
             _applicationDbContext.SaveChanges();
            return Ok(ocjena);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOcjena(int id)
        {
            var ocjena = _applicationDbContext.Ocjenas.Find(id);


            if (ocjena == null)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(ocjena);
            _applicationDbContext.SaveChanges();
            return Ok(ocjena);
        }

        
    }
}
