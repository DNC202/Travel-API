using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Tours;
using Tour_API.Helpers;
using Tour_API.Interfaces;
using Tour_API.Mappers;
using Tour_API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tour_API.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly ApplicationDBContext _context;
        public TourService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Tour>> GetAllAsync(QueryObject query)
        {
            var tours = _context.Tours.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                tours = tours.Where(t => t.Title!.Contains(query.Title!));
            }
            if (query.DestinationId.HasValue && !query.DestinationId.Equals(0))
            {
                tours = tours.Where(t => t.DestinationId.Equals(query.DestinationId));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(t => t.Price) : tours.OrderBy(t => t.Price);
                }
                if (query.SortBy.Equals("Rating", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(t => t.Rating) : tours.OrderBy(t => t.Rating);
                }
            }
            return await tours.ToListAsync();
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
            if (updateTour == null) return null!;
            updateTour.Title = tourDto.Title;
            updateTour.DestinationId = tourDto.DestinationId;
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
            if (deleteTour == null) { return null!; }
            _context.Tours.Remove(deleteTour);
            await _context.SaveChangesAsync();
            return deleteTour;
        }
    }
}
