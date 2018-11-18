using System;
using System.Threading.Tasks;

namespace RowHouseTurnManagement.Application
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public RegistrationService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Guid> AddApartment(string lastName, string streetAddress, int postalCode)
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
            bool rowHouseCreated = await _apartmentRepository.HasRowHouse(postalCode, rowHouseApartment.RowAddress);
            if (!rowHouseCreated)
            {
                await _apartmentRepository.AddRowHouse(postalCode, rowHouseApartment.RowAddress);
            }
            return await _apartmentRepository.AddApartment(postalCode, rowHouseApartment.RowAddress, lastName, rowHouseApartment.ApartmentNumber);
        }
    }
}
