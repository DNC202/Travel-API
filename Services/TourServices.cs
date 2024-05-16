using Microsoft.AspNetCore.Http.HttpResults;
using Tour_API.Data;
using Tour_API.DTOs.Tours;
using Tour_API.Models;

namespace Tour_API.Services
{
    public class TourServices
    {
        public List<Tour> tours = new List<Tour>();
        public readonly TourContext _context;
        public TourServices(TourContext context)
        {
            _context = context;
            /*tours.Add(new Tour(
                1,
                "Mountain Trek",
                ["Mountain", "Trekking"],
                //destination: 1,
                4.7,
                99.99,
                "5 days 4 nights",
                "https://travelwp.physcode.com/main-demo/wp-content/uploads/sites/7/2016/11/c9f347c4-8b17-47b5-8594-a6947299d51f-430x323.jpg"));

            tours.Add(new Tour(
                2,
                "Beachside Getaway",
                ["Beach", "Relaxation"],
                //destination: 2,
                4.5,
                79.99,
                "3 days 2 nights",
                "https://travelwp.physcode.com/main-demo/wp-content/uploads/sites/7/2016/11/c9f347c4-8b17-47b5-8594-a6947299d51f-430x323.jpg"));*/
        }

        public List<Tour> GetAllTours() => tours;

        public Tour? GetTourById(int id) => tours.FirstOrDefault(p => p.Id == id);

        public void AddTour(TourDto tour)
        {
            Tour newTour = new()
            {
                Title = tour.Title,
                Categories = tour.Categories,
                Destination = _context.Destinations.Find(tour.DestinationId),
                DestinationId = tour.DestinationId,
                Rating = tour.Rating,
                Price = tour.Price,
                Duration = tour.Duration,
                Thumbnail = tour.Thumbnail,
            };

            _context.Tours.Add(newTour);
            _context.SaveChanges();
        }

        public void EditTour(int id, Tour tour)
        {
            var index = tours.FindIndex(tour => tour.Id == id);
            if (index == -1) return;
            tours[index] = tour;
        }

        public void DeleteTour(int id)
        {
            var deleteTour = tours.FirstOrDefault(tour => tour.Id == id);
            if (deleteTour == null) return;
            tours.Remove(deleteTour);
        }
    }
}
