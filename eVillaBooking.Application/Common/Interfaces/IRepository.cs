using eVillaBooking.Domain.Entities;
using System.Linq.Expressions;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Add(T entity);
        void Delete(T entity);
        void Remove(T entity);
    }
}
