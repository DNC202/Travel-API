using System.ComponentModel.DataAnnotations;

namespace Tour_API.DTOs.Tours
{
    public class CreateTourDto
    {
        
        private string? title;
        private string[]? categories;
        
        private int destinationId;
        
        private double rating;
        
        private double price;
        
        private string? duration;
        private string? thumbnail;

        [Required]
        [MinLength(10, ErrorMessage = "Title must be 10 characters")]
        [MaxLength(200, ErrorMessage = "Title cannot be over 200 characters")]
        public string? Title { get => title; set => title = value; }
        [Required]
        public string[]? Categories { get => categories; set => categories = value; }
        [Required]
        public int DestinationId { get => destinationId; set => destinationId = value; }
        [Range(1, 5)]
        public double Rating { get => rating; set => rating = value; }
        [Required]
        [Range(10, 999)]
        public double Price { get => price; set => price = value; }
        [Required]
        public string? Duration { get => duration; set => duration = value; }
        public string? Thumbnail { get => thumbnail; set => thumbnail = value; }
    }
}
