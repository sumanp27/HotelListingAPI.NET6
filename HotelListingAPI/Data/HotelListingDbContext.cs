using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HotelListingAPI.Data
{
    public class HotelListingDbContext:DbContext
    {
        public HotelListingDbContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "India",
                    ShortName = "In"

                },
                 new Country
                 {
                     Id = 2,
                     Name = "USA",
                     ShortName = "US"

                 },
                 new Country
                 {
                     Id = 3,
                     Name = "Australia",
                     ShortName = "Au"

                 }
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Taj",
                    Address = "Bombay",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Icon",
                    Address = "Banglore",
                    CountryId = 3,
                    Rating = 4.3
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Marriot",
                    Address = "Hyderabad",
                    CountryId = 2,
                    Rating = 4.7
                }
                );
        }

    }
}
