using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RowHouseTurnManagement.Application
{
    public interface IApartmentRepository
    {
        Task<Guid> AddApartment(int postalCode, string rowAddress, string lastName, int apartmentNumber);
        Task AddRowHouse(int postalCode, string address);
        Task<bool> HasRowHouse(int postalCode, string address);
    }
}
