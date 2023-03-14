using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.Entity<Service>()
           .Property(s => s.Price)
           .HasColumnType("decimal(18,4)");

            //Provider&Service
            modelBuilder.Entity<Provider>()
           .HasMany(p => p.Services)
           .WithMany(s => s.Providers)
           .UsingEntity(j => j.ToTable("ProviderService"));


            //Customer&Service

            modelBuilder.Entity<Order>()
        .HasKey(o => new { o.CustomerId, o.ServiceId });

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Service)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.ServiceId);

            //Provider & Report
            modelBuilder.Entity<Report>()
              .HasOne(r => r.Provider)
              .WithMany(p => p.Reports)
              .HasForeignKey(r => r.ProviderId);

            //Customer& Report
            modelBuilder.Entity<Report>()
              .HasOne(r => r.Customer)
              .WithMany(c => c.Reports)
              .HasForeignKey(r => r.CustomerId);

            //Service& Report
            modelBuilder.Entity<Report>()
             .HasOne(r => r.Service)
             .WithMany(s => s.Reports)
             .HasForeignKey(r => r.ServiceId);

            modelBuilder.Entity<Booking>()
                .HasOne(s => s.Customer)
                .WithMany(r => r.Bookings)
                .HasForeignKey(s => s.CustomerId);


            modelBuilder.Entity<Booking>()
                 .HasOne(s => s.Service)
                 .WithMany(r => r.Bookings)
                 .HasForeignKey(s => s.ServiceId);

            modelBuilder.Entity<Booking>()
                .HasOne(s => s.Provider)
                .WithMany(r => r.Bookings)
                .HasForeignKey(s =>s.ProviderId);


            modelBuilder.Entity<Provider>()
               .HasOne(p => p.Admin)
               .WithMany(a => a.Providers)
              .HasForeignKey(p => p.AdminId);

        }
       
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Service> Services { get;set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Admin> Admin { get; set; }
   

    }
}
