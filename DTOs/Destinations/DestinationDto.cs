using Tour_API.DTOs.Tours;
using Tour_API.Models;

namespace Tour_API.DTOs.Destinations
{
    public class DestinationDto
    {
        private int id;
        private string? name;
        private string? image;
        private string? description;
        private List<TourDto>? tours;
        public int Id { get => id; set => id = value; }
        public string? Name { get => name; set => name = value; }
        public string? Image { get => image; set => image = value; }
        public string? Description { get => description; set => description = value; }
        public List<TourDto>? Tours { get => tours; set => tours = value; }

    }
}
