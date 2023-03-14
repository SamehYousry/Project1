namespace Project1.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Time { get; set; }
        public string Problem { get; set; }

        public string location { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }




    }
}
