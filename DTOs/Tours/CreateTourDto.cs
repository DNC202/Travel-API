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

        public string? Title { get => title; set => title = value; }
        public string[]? Categories { get => categories; set => categories = value; }
        public int DestinationId { get => destinationId; set => destinationId = value; }
        public double Rating { get => rating; set => rating = value; }
        public double Price { get => price; set => price = value; }
        public string? Duration { get => duration; set => duration = value; }
        public string? Thumbnail { get => thumbnail; set => thumbnail = value; }
    }
}
