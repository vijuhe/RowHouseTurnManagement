using System;
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
        public Guid AddApartment(Apartment apartment)
        {
            return _registrationService.AddApartment(apartment.LastName, apartment.StreetAddress, apartment.PostalCode);
        }
    }
}