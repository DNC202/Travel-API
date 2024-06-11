using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Tours;
using Tour_API.Helpers;
using Tour_API.Interfaces;
using Tour_API.Mappers;
using Tour_API.Models;
using Tour_API.Services.UploadFileServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tour_API.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly ApplicationDBContext _context;
        private readonly IUploadFileService _uploadFileService;
        public TourService(ApplicationDBContext context, IUploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
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
            /*return await tours.ToListAsync();*/
            List<Tour> listTours = await tours.ToListAsync();
            foreach(var tour in listTours)
            {
                var sasUri = await _uploadFileService.GetBlobUriWithSasToken(tour.Thumbnail!);
                tour.Thumbnail = sasUri;
            }
            return listTours;

        }
        public async Task<Tour> GetByIdAsync(int id)
        {
            return await _context.Tours.FirstAsync(c => c.Id == id);
        }
        public async Task<Tour> CreateAsync(CreateTourDto tourDto, IFormFile file)
        {
            var uri = await _uploadFileService.UploadFileAsync(file);
            tourDto.Thumbnail = uri;
            var newTour = tourDto.FromCreateDtoToTour();
            await _context.Tours.AddAsync(newTour);
            await _context.SaveChangesAsync();
            return newTour;
        }

        public async Task<Tour> UpdateAsync(int id, UpdateTourDto tourDto, IFormFile file)
        {
            var updateTour = await _context.Tours.FirstOrDefaultAsync(c => c.Id == id);
            if (updateTour == null) 
                return null!;
            // get blobname from url image
            Uri uri = new Uri(updateTour.Thumbnail!);
            string blobName = uri.Segments.Last();
            // delete blob file
            await _uploadFileService.DeleteFileAsync(blobName);

            // upload new file
            var newImageUri = await _uploadFileService.UploadFileAsync(file);

            updateTour.Title = tourDto.Title;
            updateTour.DestinationId = tourDto.DestinationId;
            updateTour.Rating = tourDto.Rating;
            updateTour.Price = tourDto.Price;
            updateTour.Duration = tourDto.Duration;
            updateTour.Thumbnail = newImageUri;
            await _context.SaveChangesAsync();
            return updateTour;
        }

        public async Task<Tour> DeleteAsync(int id)
        {
            var deleteTour = await _context.Tours.FirstOrDefaultAsync(c => c.Id == id);
            // check tour exists or not
            if (deleteTour == null)  
                return null!;
            // get blobname from url image
            Uri uri = new Uri(deleteTour.Thumbnail!);
            string blobName = uri.Segments.Last();
            // delete blob file
            await _uploadFileService.DeleteFileAsync(blobName);
            // delete tour
            _context.Tours.Remove(deleteTour);
            await _context.SaveChangesAsync();
            return deleteTour;
        }
    }
}
