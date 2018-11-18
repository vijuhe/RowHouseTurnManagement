using System;
using System.Threading.Tasks;

namespace RowHouseTurnManagement.Application
{
    public interface IRegistrationService
    {
        Task<Guid> AddApartment(string lastName, string streetAddress, int postalCode);
    }
}
