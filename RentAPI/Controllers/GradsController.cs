﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
using RentAPI.NewFolder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradsController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public GradsController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }



        [HttpGet]

        public List<Grad> GetAllGrads()
        {
            var data = _applicationDbContext.Grads
                .OrderBy(s => s.GradId).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<Grad> AddGrad([FromBody] GradsVM x)
        {
            var newGrad = new Grad
            {
                ImeGrada = x.ImeGrada,
                PostanskiKod = x.PostanskiKod,
            };

            _applicationDbContext.Add(newGrad);
            _applicationDbContext.SaveChanges();
            return newGrad;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetGrad([FromRoute] int id)
        {
            var grad = _applicationDbContext.Grads.FirstOrDefault(x => x.GradId == id);

            if (grad == null)
                return NotFound();

            return Ok(grad);
        }

        [HttpPost("{id}")]
        public ActionResult UpdateGrad(int id,[FromBody] GradsVM x)
        {
            Grad grad;

            if (id == 0)
            {
                grad = new Grad
                {
                    ImeGrada = "",
                    PostanskiKod=""
                };
                _applicationDbContext.Add(grad);
            }
            else
            {
                grad = _applicationDbContext.Grads.FirstOrDefault(x => x.GradId == id);
                if (grad == null)
                    return BadRequest("Pogresan ID");
            }

            grad.ImeGrada= x.ImeGrada;
            grad.PostanskiKod = x.PostanskiKod;

            _applicationDbContext.SaveChanges();
            return Ok(grad);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGrad(int id)
        {
            var grad = _applicationDbContext.Grads.Find(id);


            if (grad == null)
            {
                return NotFound();
            }

            _applicationDbContext.Grads.Remove(grad);
            _applicationDbContext.SaveChanges();
            return Ok(grad);
        }
    }
}
