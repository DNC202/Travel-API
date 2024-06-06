using Tour_API.DTOs.Tours;
using Tour_API.Models;

namespace Tour_API.Mappers
{
    public static class TourMappers
    {
        public static TourDto ToTourDto(this Tour tourModel)
        {
            return new TourDto
            {
                Id = tourModel.Id,
                Title = tourModel.Title,
                DestinationId = tourModel.DestinationId,
                Rating = tourModel.Rating,
                Price = tourModel.Price,
                Duration = tourModel.Duration,
                Thumbnail = tourModel.Thumbnail,
            };
        }

        public static Tour FromCreateDtoToTour(this CreateTourDto tourDto)
        {
            return new Tour
            {
                Title = tourDto.Title,
                DestinationId = tourDto.DestinationId,
                Rating = tourDto.Rating,
                Price = tourDto.Price,
                Duration = tourDto.Duration,
                Thumbnail = tourDto.Thumbnail,
            };
        }
    }
}
