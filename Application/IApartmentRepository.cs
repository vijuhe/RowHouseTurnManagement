using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RowHouseTurnManagement.Application
{
    public interface IApartmentRepository
    {
        Task<Guid> AddApartment(string lastName, string rowId, int apartmentNumber, int postalCode);
    }
}
