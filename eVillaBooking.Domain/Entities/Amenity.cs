using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eVillaBooking.Domain.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
         
        //Navigate Property
        [ValidateNever]
        public Villa Villa { get; set; }
        [ForeignKey("Villa")]
        public int VillaId { get; set; }

    }
}
