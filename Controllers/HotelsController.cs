using Microsoft.AspNetCore.Mvc;
using tecnical1.Models;

namespace tecnical1.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public HotelsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Hotel hotel) 
        {
            var estatus1 = context.Entry(hotel).State;
            context.Add(hotel);
            var estatus2 = context.Entry(hotel).State;
            await context.SaveChangesAsync();
            var estatus3 = context.Entry(hotel).State;
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(Hotel[] hotels)
        {
            context.AddRange(hotels);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
