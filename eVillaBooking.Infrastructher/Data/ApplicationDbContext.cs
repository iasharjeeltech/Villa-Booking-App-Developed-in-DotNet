using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eVillaBooking.Infrastructher.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Villa> MyProperty { get; set; }
        public DbSet<VillaNumber> VillaNumber { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        //FLuent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var seedData = new List<Villa>()
            {
                                  new Villa(){
                                              Id = 1,
                                              Name = "Royal Villa",
                                              Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                                              ImageUrl = "https://placehold.co/600x400",
                                              Occupancy = 4,
                                              Price = 200,
                                              Sqft = 550,
                                          },
                                        new Villa()
                                        {
                                            Id = 2,
                                            Name = "Premium Pool Villa",
                                            Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                                            ImageUrl = "https://placehold.co/600x401",
                                            Occupancy = 4,
                                            Price = 300,
                                            Sqft = 550,
                                        },
                                        new Villa()
                                        {
                                            Id = 3,
                                            Name = "Luxury Pool Villa",
                                            Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                                            ImageUrl = "https://placehold.co/600x402",
                                            Occupancy = 4,
                                            Price = 400,
                                            Sqft = 750,
                                        }
};
            //Data Seeding
            modelBuilder.Entity<Villa>().HasData(seedData);

            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber()
                {
                    Villa_Id = 1,
                    Villa_Number = 101,
                },
                new VillaNumber()
                {
                    Villa_Id = 2,
                    Villa_Number = 102,
                },
                new VillaNumber()
                {
                    Villa_Id = 3,
                    Villa_Number = 103,
                }
                );

            modelBuilder.Entity<Amenity>().HasData(
    new Amenity { Id = 1, Name = "Private Pool", VillaId = 1 },
    new Amenity { Id = 2, Name = "Microwave", VillaId = 1 },
    new Amenity { Id = 3, Name = "Private Balcony", VillaId = 1 },
    new Amenity { Id = 4, Name = "1 king bed and 1 sofa bed", VillaId = 1 },
    new Amenity { Id = 5, Name = "Private Plunge Pool", VillaId = 2 },
    new Amenity { Id = 6, Name = "Microwave and Mini Refrigerator", VillaId = 2 },
    new Amenity { Id = 7, Name = "Private Balcony", VillaId = 2 },
    new Amenity { Id = 8, Name = "King bed or 2 double beds", VillaId = 2 },
    new Amenity { Id = 9, Name = "Private Pool", VillaId = 3 },
    new Amenity { Id = 10, Name = "Jacuzzi", VillaId = 3 },
    new Amenity { Id = 11, Name = "Private Balcony", VillaId = 3 }
);

        }
    }
}


