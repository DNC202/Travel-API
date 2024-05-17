using System.ComponentModel.DataAnnotations;

namespace Tour_API.DTOs.Destinations
{
    public class CreateDestinationDto
    {
        private string? name;
        private string? image;
        private string? description;

        [Required]
        [MinLength(3, ErrorMessage = "Name must be 3 characters")]
        [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
        public string? Name { get => name; set => name = value; }
        public string? Image { get => image; set => image = value; }
        [Required]
        [MinLength(10, ErrorMessage = "Decription must be 10 characters")]
        [MaxLength(200, ErrorMessage = "Description cannot be over 200 characters")]
        public string? Description { get => description; set => description = value; }
    }
}
