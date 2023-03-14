namespace Project1.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int ProviderId { get; set; }
        public ICollection<Provider> Providers { get; set; }
        public ICollection<Order> Orders { get; set; }

        public List<Report> Reports { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
