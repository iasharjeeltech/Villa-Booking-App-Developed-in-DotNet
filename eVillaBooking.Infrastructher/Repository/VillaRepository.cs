using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructher.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eVillaBooking.Infrastructher.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {

        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db): base(db)   
        {
            _db = db;

        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Villa villa)
        {
            _db.MyProperty .Update(villa);
        }

    }
}
