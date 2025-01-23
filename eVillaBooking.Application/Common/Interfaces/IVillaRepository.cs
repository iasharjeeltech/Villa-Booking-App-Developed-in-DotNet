using eVillaBooking.Domain.Entities;
using System.Linq.Expressions;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        void Update(Villa villa);

    }
}
