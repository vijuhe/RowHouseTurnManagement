using System;
using Affecto.Identifiers.Finnish;
using NSubstitute;
using RowHouseTurnManagement.Application;
using Xunit;

namespace RowHouseTurnManagement.UnitTests
{
    public class AddApartmentTests
    {
        private readonly RegistrationService _registrationService;
        private readonly IApartmentRepository _apartmentRepository;

        public AddApartmentTests()
        {
            _apartmentRepository = Substitute.For<IApartmentRepository>();
            _registrationService = new RegistrationService(_apartmentRepository);
        }

        [Fact]
        public async System.Threading.Tasks.Task ApartmentCannotBeAddedWithNullLastName()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment(null, "Street 1 A 12", PostalCode.Create("12345")));
        }

        [Fact]
        public async System.Threading.Tasks.Task ApartmentCannotBeAddedWithEmptyLastName()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment(string.Empty, "Street 1 A 12", PostalCode.Create("12345")));
        }

        [Fact]
        public async System.Threading.Tasks.Task ApartmentCannotBeAddedWithNullStreetAddress()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment("Heikkinen", null, PostalCode.Create("12345")));
        }

        [Fact]
        public async System.Threading.Tasks.Task ApartmentCannotBeAddedWithEmptyStreetAddress()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment("Heikkinen", string.Empty, PostalCode.Create("12345")));
        }

        [Theory]
        [InlineData("Tampurinkatu 2 C 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2 C as 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2 C as. 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2 C AS. 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2C12", "Tampurinkatu 2C", 12)]
        public void ApartmentNumberIsSeparatedFromTheRestOfTheAddress(string streetAddress, string rowAddress, int apartmentNumber)
        {
            _registrationService.AddApartment("Heikkinen", streetAddress, PostalCode.Create("20780"));

            _apartmentRepository.Received(1).AddApartment("Heikkinen", rowAddress, apartmentNumber, 20780);
        }
    }
}
