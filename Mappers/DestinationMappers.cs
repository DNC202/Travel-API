using Tour_API.DTOs.Destinations;
using Tour_API.Models;

namespace Tour_API.Mappers
{
    public static class DestinationMappers
    {
        public static DestinationDto ToDestinationDto(this Destination destinationModel)
        {
            return new DestinationDto
            {
                Id = destinationModel.Id,
                Name = destinationModel.Name,
                Description = destinationModel.Description,
                Image = destinationModel.Image,
                Tours = destinationModel.Tours?.Select(c => c.ToTourDto()).ToList(),
            };
        }

        public static Destination FromCreateDtoToDestination(this CreateDestinationDto destinationDto)
        {
            return new Destination
            {
                Name = destinationDto.Name,
                Description = destinationDto.Description,
                Image = destinationDto.Image,
            };
        }
    }
}
