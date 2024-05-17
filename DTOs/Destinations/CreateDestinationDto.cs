namespace Tour_API.DTOs.Destinations
{
    public class CreateDestinationDto
    {
        private string? name;
        private string? image;
        private string? description;

        public string? Name { get => name; set => name = value; }
        public string? Image { get => image; set => image = value; }
        public string? Description { get => description; set => description = value; }
    }
}
