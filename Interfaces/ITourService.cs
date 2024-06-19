using Tour_API.DTOs.Tours;
using Tour_API.Helpers;
using Tour_API.Models;

namespace Tour_API.Interfaces
{
    public interface ITourService
    {
        Task<List<Tour>> GetAllAsync(QueryObject query);
        Task<Tour> GetByIdAsync(int id);
        Task<Tour> CreateAsync(Tour newTour, IFormFile file);
        Task<Tour> UpdateAsync(int id, UpdateTourDto TourDto, IFormFile file);
        Task<Tour> DeleteAsync(int id);
    }
}
