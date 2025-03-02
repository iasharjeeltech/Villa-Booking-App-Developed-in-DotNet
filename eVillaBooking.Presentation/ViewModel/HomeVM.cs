using eVillaBooking.Domain.Entities;

namespace eVillaBooking.Presentation.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Villa>? VillaList { get; set; }
        public int Nights { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }

    }
}
