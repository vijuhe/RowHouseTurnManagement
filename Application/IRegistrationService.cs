using System;
using System.Threading.Tasks;
using Affecto.Identifiers.Finnish;

namespace RowHouseTurnManagement.Application
{
    public interface IRegistrationService
    {
        Task<Guid> AddApartment(string lastName, string streetAddress, PostalCode postalCode);
    }
}
