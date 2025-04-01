namespace eVillaBooking.Application.Common.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    void Update(Booking booking);
}
