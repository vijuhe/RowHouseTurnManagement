using System;
using System.Threading.Tasks;
using Affecto.Identifiers.Finnish;

namespace RowHouseTurnManagement.Application
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public RegistrationService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Guid> AddApartment(string lastName, string streetAddress, PostalCode postalCode)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException();
            }
            if (string.IsNullOrWhiteSpace(streetAddress))
            {
                throw new ArgumentException();
            }

            var rowHouseApartment = RowHouseApartment.Create(streetAddress);
            return await _apartmentRepository.AddApartment(lastName, rowHouseApartment.RowAddress, rowHouseApartment.ApartmentNumber, 
                int.Parse(postalCode.ToString()));
        }
    }
}
