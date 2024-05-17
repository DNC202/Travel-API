using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Destinations;
using Tour_API.Models;

namespace Tour_API.Services.DestinationServices
{
    public class DestinationService : IDestinationService
    {
        private readonly TourContext _context;
        public DestinationService(TourContext context) 
        {
            _context = context;
        }
        public async Task<List<Destination>> GetAllAsync()
        {
            return await _context.Destinations.Include(c => c.Tours).ToListAsync();
        }
        public async Task<Destination> GetByIdAsync(int id)
        {
            return await _context.Destinations.Include(c => c.Tours).FirstAsync(c => c.Id == id);
        }
        public async Task<Destination> CreateAsync(Destination destination)
        {
            await _context.Destinations.AddAsync(destination);
            await _context.SaveChangesAsync();
            return destination;
        }

        public async Task<Destination> UpdateAsync(int id, UpdateDestinationDto destinationDto)
        {
            var updateDestination = await _context.Destinations.FirstOrDefaultAsync(c => c.Id == id);
            if (updateDestination == null) return null;
            updateDestination.Name = destinationDto.Name;
            updateDestination.Description = destinationDto.Description;
            updateDestination.Image = destinationDto.Image;
            await _context.SaveChangesAsync();
            return updateDestination;
        }

        public async Task<Destination> DeleteAsync(int id)
        {
            var deleteDestination = await _context.Destinations.FirstOrDefaultAsync(c => c.Id == id);
            if (deleteDestination == null) { return null; }
            _context.Destinations.Remove(deleteDestination);
            await _context.SaveChangesAsync();
            return deleteDestination;
        }
    }
}
