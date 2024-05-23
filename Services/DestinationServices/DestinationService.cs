using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Destinations;
using Tour_API.Models;

namespace Tour_API.Services.DestinationServices
{
    public class DestinationService : IService
    {
        private readonly TourContext _context;
        public DestinationService(TourContext context) 
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync<T>()
        {
            List<Destination> destinations = await _context.Destinations.Include(c => c.Tours).ToListAsync();
            return destinations.Cast<T>().ToList();
        }
        public async Task<T> GetByIdAsync<T>(int id)
        {
            Destination destination = await _context.Destinations.Include(c => c.Tours).FirstAsync(c => c.Id == id);
            return (T)(object)destination;
        }
        public async Task<T> CreateAsync<T>(T model)
        {
            var destination = (Destination)(object)model!;
            await _context.Destinations.AddAsync(destination);
            await _context.SaveChangesAsync();
            return (T)(object)destination;
        }

        public async Task<T> UpdateAsync<T>(int id, T model)
        {
            var updateDestination = await _context.Destinations.FirstOrDefaultAsync(c => c.Id == id);
            if (updateDestination == null) 
                return (T)(object)null!;
            var destinationDto = (UpdateDestinationDto)(object)model!;
            updateDestination.Name = destinationDto.Name;
            updateDestination.Description = destinationDto.Description;
            updateDestination.Image = destinationDto.Image;
            await _context.SaveChangesAsync();
            return (T)(object)updateDestination;
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            var deleteDestination = await _context.Destinations.FirstOrDefaultAsync(c => c.Id == id);
            if (deleteDestination == null)
                return (T)(object)null!; 
            _context.Destinations.Remove(deleteDestination);
            await _context.SaveChangesAsync();
            return (T)(object)deleteDestination;
        }
    }
}
