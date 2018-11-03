using System;

namespace RowHouseTurnManagement.Application
{
    public class RegistrationService : IRegistrationService
    {
        public Guid AddApartment(string lastName, string streetAddress, int postalCode)
        {
            return Guid.NewGuid();
        }
    }
}
