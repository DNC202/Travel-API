namespace Tour_API.Models
{
    public class Tour
    {
        private int id;
        private string? title;
        private int destinationId;
        private double rating;
        private double price;
        private string? duration;
        private string? thumbnail;
        private Destination? destination;


        public int Id { get => id; set => id = value; }
        public string? Title { get => title; set => title = value; }
        public int DestinationId { get => destinationId; set => destinationId = value; }
        public double Rating { get => rating; set => rating = value; }
        public double Price { get => price; set => price = value; }
        public string? Duration { get => duration; set => duration = value; }
        public string? Thumbnail { get => thumbnail; set => thumbnail = value; }
        public Destination? Destination { get => destination; set => destination = value; }
    }
}
