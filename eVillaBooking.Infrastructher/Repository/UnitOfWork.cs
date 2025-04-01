using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Infrastructher.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;                      
using System.Threading.Tasks;

namespace eVillaBooking.Infrastructher.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IVillaNumberRepository VillaNumbersRepositoryUOW { get; private set; }

        public IVillaRepository VillaRepositoryUOW { get; private set; }

        public IAmenityRepository AmenityRepositoryUOW { get; private set; }

        public IBookingRepository BookingRepositUOW { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            this.VillaRepositoryUOW = new VillaRepository(db);
            this.VillaNumbersRepositoryUOW = new VillaNumberRepository(db);
            this.AmenityRepositoryUOW = new AmenityRepository(db);
            this.BookingRepositoryUOW = new BookingRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();

        }
    }
}


