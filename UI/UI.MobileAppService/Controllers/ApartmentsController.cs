using System;
using System.Threading.Tasks;
using Affecto.Identifiers.Finnish;
using Microsoft.AspNetCore.Mvc;
using RowHouseTurnManagement.Application;
using UI.MobileAppService.Models;

namespace UI.MobileAppService.Controllers
{
    [Produces("application/json")]
    [Route("api/Apartments")]
    public class ApartmentsController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public ApartmentsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<Guid> AddApartment([FromBody] Apartment apartment)
        {
            return await _registrationService.AddApartment(apartment.LastName, apartment.StreetAddress, PostalCode.Create(apartment.PostalCode.ToString()));
        }
    }
}