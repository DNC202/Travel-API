using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour_API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly TourContext _context;
        public DestinationsController(TourContext context)
        {
            _context = context;
        }
        // GET: api/<DestinationsController>
        [HttpGet]
        public IActionResult GetAll()
        {   
            var Destinations = _context.Destinations.Include(c => c.Tours).ToList();
            return Ok(Destinations);
        }

        // GET api/<DestinationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DestinationsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DestinationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DestinationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
