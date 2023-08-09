using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListingAPI.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
