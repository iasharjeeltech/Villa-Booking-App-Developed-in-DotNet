﻿using eVillaBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eVillaBooking.Infrastructher.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Villa> MyProperty { get; set; }
        public DbSet<VillaNumber> VillaNumber { get; set; }
        //FLuent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            //base.OnModelCreating(modelBuilder);


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

        }
    }
}


