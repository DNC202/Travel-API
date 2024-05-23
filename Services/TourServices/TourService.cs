using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Tours;
using Tour_API.Models;

namespace Tour_API.Services.TourServices
{
    public class TourService : IService
    {
        private readonly TourContext _context;
        public TourService(TourContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync<T>()
        {
            var tours = await _context.Tours.ToListAsync();
            return tours.Cast<T>().ToList();
        }
        public async Task<T> GetByIdAsync<T>(int id)
        {
            var tour = await _context.Tours.FirstAsync(c => c.Id == id);
            return (T)(object)tour;
        }
        public async Task<T> CreateAsync<T>(T model)
        {
            var tour = (Tour)(object)model!;
            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();
            return (T)(object)tour;
        }

        public async Task<T> UpdateAsync<T>(int id, T model)
        {
            var updateTour = await _context.Tours.FirstOrDefaultAsync(c => c.Id == id);
            if (updateTour == null) 
                return (T)(object)null!;
            var tourDto = (Tour)(object)model!;
            updateTour.Title = tourDto.Title;
            updateTour.DestinationId = tourDto.DestinationId;
            updateTour.Categories = tourDto.Categories;
            updateTour.Rating = tourDto.Rating;
            updateTour.Price = tourDto.Price;
            updateTour.Duration = tourDto.Duration;
            updateTour.Thumbnail = tourDto.Thumbnail;
            await _context.SaveChangesAsync();
            return (T)(object)updateTour;
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            var deleteTour = await _context.Tours.FirstOrDefaultAsync(c => c.Id == id);
            if (deleteTour == null)  
                return (T)(object)null!; 
            _context.Tours.Remove(deleteTour);
            await _context.SaveChangesAsync();
            return (T)(object)deleteTour;
        }
    }
}
