using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eVillaBooking.Domain.Entities
{
    public class Booking
    {
        //primary key for the booking entity 
        public int Id { get; set; }

        //navigation property for the user who made the booking
        public ApplicationUser User { get; set; }

        //foreign key for user
        [Required, ForeignKey(nameof(User))]
        public string UserId { get; set; }
       
        //navigation property for the booked villa
        public Villa Villa { get; set; }

        //Foreign Key for Villa
        [Required, ForeignKey(nameof(Villa))]
        public int VillaId { get; set; }

        //name of the person making the booking
        [Required]
        public string Name { get; set; }
        
        //email of the person who making the booking
        [Required]
        public string Email {  get; set; }

        //optional phone number of the person making the booking
        public string? Phone {  get; set; }

        //total cost of the booking
        [Required]
        public double? TotalCost { get; set; }

        //number of nighte the villa is booked for 
        public int Nights { get; set; }

        //status of the booking (eg: confirmed, pending , canceled)
        public string? Status { get; set; }

        //date when the booking was made
        [Required]
        public DateTime BookingDate { get; set; }

        //date of checkIn
        [Required]
        public DateTime CheckInDate { get; set; }

        //Date of CheckOut
        [Required]
        public DateTime CheckOutDate { get; set; }

        // Indicates whether the payment
        public bool IsPaymentSuccessfull { get; set; } = false;

        // Date of successful payment
        public DateTime PaymentDate { get; set; }

        // Stripe session ID for tracking payments
        public string? StripeSessionId { get; set; }

        // Stripe payment intent ID for tracking transaction
        public string? StripePaymentIntentId { get; set; }

        // Actual check—in date (useful for tracking any changes to the planned check—in)
        public DateTime ActualCheckInDate { get; set; }

        // Actual check—out date (useful for tracking any changes to the planned check—out)
        public DateTime ActualCheckOutDate { get; set; }

        // The specific villa number assigned to this booking
        public int VillaNumber { get; set; }
    }
}
