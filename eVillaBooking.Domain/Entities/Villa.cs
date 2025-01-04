using System.ComponentModel.DataAnnotations;

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
        public string? ImageUrl {  get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
