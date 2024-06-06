using Tour_API.DTOs.Destinations;
using Tour_API.Helpers;
using Tour_API.Models;

namespace Tour_API.Interfaces
{
    public interface IDestinationService
    {
        Task<List<Destination>> GetAllAsync();
        Task<Destination> GetByIdAsync(int id);
        Task<Destination> CreateAsync(Destination destinationModel);
        Task<Destination> UpdateAsync(int id, UpdateDestinationDto destinationDto);
        Task<Destination> DeleteAsync(int id);
    }
}
