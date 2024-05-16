namespace Tour_API.Models
{
    public class Destination
    {
        private int id;
        private string? name;
        private string? image;
        private string? description;
        private List<Tour>? tours;
        
        public int Id { get => id; set => id = value; }
        public string? Name { get => name; set => name = value; }
        public string? Image { get => image; set => image = value; }
        public string? Description { get => description; set => description = value; }
        public List<Tour>? Tours { get => tours; set => tours = value; }

    }
}
