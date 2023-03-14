namespace Project1.Models
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Service { get; set; }
        public byte[] ImageProvider { get; set; }
        public byte[] ImageFront { get; set; }
        public byte[] ImageBack { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public List<Booking> Bookings { get; set; }
        public ICollection<Service> Services { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public List<Report> Reports { get; set; }   
    }
}
