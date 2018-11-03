using System;

namespace RowHouseTurnManagement.Application
{
    public interface IRegistrationService
    {
        Guid AddApartment(string lastName, string streetAddress, int postalCode);
    }
}
