using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructher.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eVillaBooking.Infrastructher.Repository
{
    public class VillaRepository : IVillaRepository
    {

        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Villa villa)
        {
            _db.MyProperty.Add(villa);
        }

        public void Remove(Villa villa)
        {
            _db.MyProperty.Remove(villa);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Villa villa)
        {
            _db.Update(villa);
        }

        public void Delete(Villa villa)
        {
            throw new NotImplementedException();
        }

        public Villa Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.MyProperty;
           
            query = query.Where(filter);
           
            if (includeProperties is not null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }

                return query.FirstOrDefault();
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<Villa>GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.MyProperty;
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            if(includeProperties is not null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToList();
        }

    }
}
