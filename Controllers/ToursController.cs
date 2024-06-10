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
                var uri = await _uploadFileService.UploadFileAsync(file);
                tourDto.Thumbnail = uri;
                var newTour = tourDto.FromCreateDtoToTour();
                await _tourService.CreateAsync(newTour);
                return CreatedAtAction(nameof(Get), new { id = newTour.Id }, newTour.ToTourDto());
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var urlFile = await _uploadFileService.UploadFileAsync(file);
                var jsonString = JsonSerializer.Serialize(urlFile);
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // PUT api/<ToursController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditTour([FromRoute] int id, [FromBody] UpdateTourDto tourDto)
        {
            var updateTour = await _tourService.UpdateAsync(id, tourDto);
            // check tour exists or not
            if (updateTour is null)
                return NotFound();
            return NoContent();
        }

        // DELETE api/<ToursController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // check tour exists or not
            var tour = await _tourService.GetByIdAsync(id);
            if (tour is null)
                return NotFound();
            // get blobname from url image
            Uri uri = new Uri(tour.Thumbnail!);
            string blobName = uri.Segments.Last();
            // delete tour
            var result = await _tourService.DeleteAsync(id);
            if(result is null)
                return NotFound();
            // delete blob file
            await _uploadFileService.DeleteFileAsync(blobName);
            return NoContent();
        }
    }
}
