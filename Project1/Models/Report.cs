namespace Project1.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string Feedback { get; set; }

        public int ProviderId { get; set; }

        public Provider Provider { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
