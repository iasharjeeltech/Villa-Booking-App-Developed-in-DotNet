using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVillaBooking.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IVillaNumberRepository VillaNumbersRepositoryUOW { get; }
        public IVillaRepository VillaRepositoryUOW { get; }
        public IAmenityRepository AmenityRepositoryUOW { get; }
        void Save();
    }
}
