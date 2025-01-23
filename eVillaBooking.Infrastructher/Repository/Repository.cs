using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Infrastructher.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eVillaBooking.Infrastructher.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal readonly  DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {

        }

        public void Remove(T entity)
        {
            //_db.Set<T>().Remove(entity);
            dbSet.Remove(entity);

        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(filter);

            if (includeProperties is not null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }

            }
            return query.FirstOrDefault()!;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;            
                if (filter is not null)
            {
                query = query.Where(filter);
            }   
            if (includeProperties is not null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToList();
        }
    }
}
