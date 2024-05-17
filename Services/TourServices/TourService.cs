using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Tours;
using Tour_API.Models;

namespace Tour_API.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly TourContext _context;
        public TourService(TourContext context)
        {
            _context = context;
        }
        public async Task<List<Tour>> GetAllAsync()
        {
            return await _context.Tours.ToListAsync();
        }
        public async Task<Tour> GetByIdAsync(int id)
        {
            return await _context.Tours.FirstAsync(c => c.Id == id);
        }
        public async Task<Tour> CreateAsync(Tour tour)
        {
            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();
            return tour;
        }

        public async Task<Tour> UpdateAsync(int id, UpdateTourDto tourDto)
        {
            var updateTour = await _context.Tours.FirstOrDefaultAsync(c => c.Id == id);
            if (updateTour == null) return null;
            updateTour.Title = tourDto.Title;
            updateTour.DestinationId = tourDto.DestinationId;
            updateTour.Categories = tourDto.Categories;
            updateTour.Rating = tourDto.Rating;
            updateTour.Price = tourDto.Price;
            updateTour.Duration = tourDto.Duration;
            updateTour.Thumbnail = tourDto.Thumbnail;
            await _context.SaveChangesAsync();
            return updateTour;
        }

        public async Task<Tour> DeleteAsync(int id)
        {
            var deleteTour = await _context.Tours.FirstOrDefaultAsync(c => c.Id == id);
            if (deleteTour == null) { return null; }
            _context.Tours.Remove(deleteTour);
            await _context.SaveChangesAsync();
            return deleteTour;
        }
    }
}
