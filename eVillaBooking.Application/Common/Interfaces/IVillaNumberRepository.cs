using eVillaBooking.Domain.Entities;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IVillaNumberRepository: IRepository<VillaNumber>
    {
        void Update(VillaNumber villaNumber);
    }
}
