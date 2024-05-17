﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs;
using Tour_API.DTOs.Tours;
using Tour_API.Mappers;
using Tour_API.Models;
using Tour_API.Services.TourServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;
        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        // GET: api/<ToursController>
        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tours = await _tourService.GetAllAsync();
            var toursDto = tours.Select(t => t.ToTourDto());
            return Ok(tours);
        }


        // GET api/<ToursController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tour = await _tourService.GetByIdAsync(id);
            if (tour is null)
                return NotFound();
            return Ok(tour.ToTourDto());
        }

        // POST api/<ToursController>
        [HttpPost("add")]
        public async Task<IActionResult> AddTour([FromBody] CreateTourDto tourDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newTour = tourDto.FromCreateDtoToTour();
            await _tourService.CreateAsync(newTour);
            return CreatedAtAction(nameof(Get), new {id = newTour.Id}, newTour.ToTourDto());
        }

        // PUT api/<ToursController>/5
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> EditTour([FromRoute] int id, [FromBody] UpdateTourDto tourDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updateTour = await _tourService.UpdateAsync(id, tourDto);
            // check tour exists or not
            if (updateTour is null)
                return NotFound();     
            return NoContent();
        }

        // DELETE api/<ToursController>/5
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check tour exists or not
            var tour = await _tourService.DeleteAsync(id);
            if (tour is null)
                return NotFound();  
            return NoContent();
        }
    }
}
