using Microsoft.AspNetCore.Identity;

namespace Project1.Models
{
    public class Order 
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
