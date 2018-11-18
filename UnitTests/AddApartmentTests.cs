using System;
using System.Threading.Tasks;
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
        public async Task ApartmentCannotBeAddedWithNullLastName()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment(null, "Street 1 A 12", 12345));
        }

        [Fact]
        public async Task ApartmentCannotBeAddedWithEmptyLastName()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment(string.Empty, "Street 1 A 12", 12345));
        }

        [Fact]
        public async Task ApartmentCannotBeAddedWithNullStreetAddress()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment("Heikkinen", null, 12345));
        }

        [Fact]
        public async Task ApartmentCannotBeAddedWithEmptyStreetAddress()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _registrationService.AddApartment("Heikkinen", string.Empty, 12345));
        }

        [Theory]
        [InlineData("Tampurinkatu 2 C 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2 C as 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2 C as. 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2 C AS. 12", "Tampurinkatu 2 C", 12)]
        [InlineData("Tampurinkatu 2C12", "Tampurinkatu 2C", 12)]
        public async Task ApartmentNumberIsSeparatedFromTheRestOfTheAddress(string streetAddress, string rowAddress, int apartmentNumber)
        {
            await _registrationService.AddApartment("Heikkinen", streetAddress, 20780);

            _apartmentRepository.Received(1).AddApartment(20780, rowAddress, "Heikkinen", apartmentNumber);
        }

        [Fact]
        public async Task RowHouseIsAddedIfItDoesNotExist()
        {
            const int postalCode = 20780;
            const string rowAddress = "Tampereentie 1 A";

            await _registrationService.AddApartment("Heikkinen", $"{rowAddress} 13", postalCode);

            _apartmentRepository.Received(1).AddRowHouse(postalCode, rowAddress);
        }

        [Fact]
        public async Task RowHouseIsNotAddedIfItDoesExist()
        {
            const int postalCode = 20780;
            const string rowAddress = "Tampereentie 1 A";
            _apartmentRepository.HasRowHouse(postalCode, rowAddress).Returns(true);

            await _registrationService.AddApartment("Heikkinen", $"{rowAddress} 13", postalCode);

            _apartmentRepository.DidNotReceive().AddRowHouse(Arg.Any<int>(), Arg.Any<string>());
        }
    }
}
