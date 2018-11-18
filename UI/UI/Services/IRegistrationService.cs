using System;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Services
{
    public interface IRegistrationService
    {
        Task<Guid> AddApartment(Apartment apartment);
    }
}
