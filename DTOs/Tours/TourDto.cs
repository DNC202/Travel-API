using Tour_API.Models;

namespace Tour_API.DTOs.Tours
{
    public class TourDto
    {
        private int id;
        private string? title;
        private string[]? categories;
        private int destinationId;
        /*private Destination? destination;*/
        private double rating;
        private double price;
        private string? duration;
        private string? thumbnail;


        public int Id { get => id; set => id = value; }
        public string? Title { get => title; set => title = value; }
        public string[]? Categories { get => categories; set => categories = value; }
        public int DestinationId { get => destinationId; set => destinationId = value; }
        /*public Destination? Destination { get => destination; set => destination = value; }*/
        public double Rating { get => rating; set => rating = value; }
        public double Price { get => price; set => price = value; }
        public string? Duration { get => duration; set => duration = value; }
        public string? Thumbnail { get => thumbnail; set => thumbnail = value; }
    }
}
