using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.IO;
using Tour_API.Data;
using Tour_API.DTOs.Destinations;
using Tour_API.DTOs.Tours;
using Tour_API.Mappers;
using Tour_API.Services.DestinationServices;
using Tour_API.Models;
using Tour_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
       
        private readonly IService _destinationService;
        public DestinationsController([FromKeyedServices("destination")] IService destinationService)
        {
            _destinationService = destinationService;

        }
        // GET: api/<DestinationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var destinations = await _destinationService.GetAllAsync<Destination>();
            var destinationDto =  destinations.Select(c => c.ToDestinationDto());
            return Ok(destinationDto);
        }

        // GET api/<DestinationsController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var destination = await _destinationService.GetByIdAsync<Destination>(id);
            if(destination is null)
                return NotFound();
            var destinationDto = destination.ToDestinationDto();
            var jsonString = JsonSerializer.Serialize(destinationDto);
            return Ok(jsonString);
        }

        // POST api/<DestinationsController>
        [HttpPost("add")]
        public async Task<IActionResult> Post(CreateDestinationDto destinationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newDestination = destinationDto.FromCreateDtoToDestination();
            await _destinationService.CreateAsync(newDestination);
            return CreatedAtAction(nameof(Get), new { id = newDestination.Id }, newDestination.ToDestinationDto());
        }

        // PUT api/<DestinationsController>/5
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateDestinationDto destinationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var destination = await _destinationService.UpdateAsync(id, destinationDto);
            if (destination is null)
                return BadRequest("Destination doesn't exist");
            return NoContent();
        }

        // DELETE api/<DestinationsController>/5
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check tour exists or not
            var destination = await _destinationService.DeleteAsync<Destination>(id);
            if (destination is null)
                return BadRequest("Destination doesn't exist");
            return NoContent();
        }
    }
}
