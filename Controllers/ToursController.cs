using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Tour_API.Data;
using Tour_API.DTOs;
using Tour_API.DTOs.Destinations;
using Tour_API.DTOs.Tours;
using Tour_API.Helpers;
using Tour_API.Interfaces;
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
        private readonly IUploadFileService _uploadFileService;   
        public ToursController(ITourService tourService, IUploadFileService uploadFileService)
        {
            _tourService = tourService;
            _uploadFileService = uploadFileService;
        }

        // GET: api/<ToursController>
        [HttpGet]
        public async Task<IActionResult> GetTours([FromQuery] QueryObject query)
        {
            var tours = await _tourService.GetAllAsync(query);
            var toursDto = tours.Select(t => t.ToTourDto());
            return Ok(tours);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour is null)
                return NotFound();
            return Ok(tour.ToTourDto());

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTour([FromForm] CreateTourDto tourDto, IFormFile file)
        {
            try
            { 
                await _tourService.CreateAsync(tourDto, file);
                return Ok(tourDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // PUT api/<ToursController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditTour([FromRoute] int id, [FromForm] UpdateTourDto tourDto, IFormFile file)
        {
            try
            {
                var updateTour = await _tourService.UpdateAsync(id, tourDto, file);
                // check tour exists or not
                if (updateTour is null)
                    return NotFound();
                return Ok(updateTour.ToTourDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        // DELETE api/<ToursController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // check tour exists or not
            var tour = await _tourService.DeleteAsync(id);
            if (tour is null)
                return NotFound();
            return NoContent();
        }
    }
}
