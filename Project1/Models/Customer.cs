namespace Project1.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public ICollection<Order> Orders { get; set; }
        public List<Report> Reports { get; set; }
        public List<Booking> Bookings { get; set; }

    }

    
}
