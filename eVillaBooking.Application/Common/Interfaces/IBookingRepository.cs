using eVillaBooking.Domain.Entities;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking booking);
        void UpdateStatus(int bookingId, string bookingStatus);
        void UpdateStripePaymentId(int bookingId, string sessionId,string paymentIntentId);
    }
}
