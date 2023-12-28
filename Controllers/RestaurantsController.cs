using Microsoft.AspNetCore.Mvc;
using tecnical1.Models;

namespace tecnical1.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public RestaurantsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Restaurant restaurant)
        {
            var estatus1 = context.Entry(restaurant).State;
            context.Add(restaurant);
            var estatus2 = context.Entry(restaurant).State;
            await context.SaveChangesAsync();
            var estatus3 = context.Entry(restaurant).State;
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(Restaurant[] restaurants)
        {
            context.AddRange(restaurants);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
