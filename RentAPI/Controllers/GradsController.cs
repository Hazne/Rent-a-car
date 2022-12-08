using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPI.Data;
using RentAPI.Models;
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
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllGrads()
        {
            var grads = await _applicationDbContext.Grads.ToListAsync();

            return Ok(grads);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrad([FromBody] Grad gradRequest)
        {

            await _applicationDbContext.Grads.AddAsync(gradRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(gradRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGrad([FromRoute] int id)
        {
            var grad = await _applicationDbContext.Grads.FirstOrDefaultAsync(x => x.GradId == id);

            if (grad == null)
                return NotFound();

            return Ok(grad);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateGrad([FromRoute] int id, Grad updateGradRequest)
        {
            var grad = await _applicationDbContext.Grads.FindAsync(id);

            if (grad == null)
            {
                return NotFound();
            }

            grad.ImeGrada = updateGradRequest.ImeGrada;
            grad.PostanskiKod = updateGradRequest.PostanskiKod;
            grad.Izdavac = updateGradRequest.Izdavac;

            await _applicationDbContext.SaveChangesAsync();
            return Ok(grad);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGrad([FromRoute] int id)
        {
            var grad = await _applicationDbContext.Grads.FindAsync(id);


            if (grad == null)
            {
                return NotFound();
            }

            _applicationDbContext.Grads.Remove(grad);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(grad);
        }
    }
}
