using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> AddApartment([FromBody] Apartment apartment)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            Guid apartmentId = await _registrationService.AddApartment(apartment.LastName, apartment.StreetAddress, apartment.PostalCode);
            return Ok(apartmentId);
        }
    }
}