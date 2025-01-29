using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVillaBooking.Domain.Entities
{
    public class Villa
    {
        public int Id {  get; set; }
        [MaxLength(30)]
        public required string  Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        [Range(10,10000)]
        public int Sqft { get; set; }
        public int Occupancy {  get; set; }
        public string? ImageUrl { get; set; } 
        [NotMapped]
        public IFormFile? Image { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }









































        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
