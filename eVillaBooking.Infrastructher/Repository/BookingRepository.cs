using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Application.Utility;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructher.Data;


namespace eVillaBooking.Infrastructher.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)  
        {
            _db = db;
        }

        public void Update(Booking booking)
        {
            _db.Bookings.Update(booking);
        }

        public void UpdateStatus(int bookingId, string bookingStatus)
        {
           Booking bookingFromDb = _db.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (bookingFromDb is not null) { 
                bookingFromDb.Status = bookingStatus;
                if (bookingFromDb.Status == StaticDetails.StatusCheckedIn)
                { 
                    bookingFromDb.ActualCheckInDate= DateTime.Now;
                }
                if(bookingFromDb.Status == StaticDetails.StatusCompleted)
                {
                    bookingFromDb.ActualCheckOutDate= DateTime.Now;
                }
            }
        }

        public void UpdateStripePaymentId(int bookingId, string sessionId, string paymentIntentId)
        {
            Booking bookingFromDb = _db.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if(bookingFromDb is not null)
            {
                if(!string.IsNullOrEmpty(sessionId))
                {
                    bookingFromDb.StripeSessionId = sessionId;
                }
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    bookingFromDb.StripePaymentIntentId = paymentIntentId;
                    bookingFromDb.PaymentDate = DateTime.Now;
                    bookingFromDb.IsPaymentSuccessfull = true;
                }
            }

        }
    }

}