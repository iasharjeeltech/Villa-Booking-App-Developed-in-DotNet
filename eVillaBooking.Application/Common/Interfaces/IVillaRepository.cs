using eVillaBooking.Domain.Entities;
using System.Linq.Expressions;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IVillaRepository
    {
        IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter=null, string? includeProperties = null);
        Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties=null);

        void Add(Villa villa);
        void Update(Villa villa);
        void Delete(Villa villa);
        void Remove(Villa villa);
        void Save();
    }
}
