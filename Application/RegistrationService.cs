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
            int postalCodeNumber = int.Parse(postalCode.ToString());
            bool rowHouseCreated = await _apartmentRepository.HasRowHouse(postalCodeNumber, rowHouseApartment.RowAddress);
            if (!rowHouseCreated)
            {
                await _apartmentRepository.AddRowHouse(postalCodeNumber, rowHouseApartment.RowAddress);
            }
            return await _apartmentRepository.AddApartment(postalCodeNumber, rowHouseApartment.RowAddress, lastName, rowHouseApartment.ApartmentNumber);
        }
    }
}
