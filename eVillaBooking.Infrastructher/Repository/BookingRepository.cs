using eVillaBooking.Application.Common.Interfaces;
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
    }

}
