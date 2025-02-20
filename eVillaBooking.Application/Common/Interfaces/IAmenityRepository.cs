using eVillaBooking.Domain.Entities;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IAmenityRepository: IRepository<Amenity>
    {
        void Update(Amenity amenity);
    }
}
