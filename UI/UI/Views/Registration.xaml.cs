using System;
using UI.Models;
using UI.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registration : ContentPage
	{
	    private readonly IRegistrationService _registrationService;

	    public Registration (IRegistrationService registrationService)
	    {
	        _registrationService = registrationService;
	        InitializeComponent();
	    }

	    private async void AddRegistration(object sender, EventArgs e)
	    {
	        var apartment = new Apartment
	        {
	            LastName = LastName.Text,
	            StreetAddress = StreetAddress.Text,
	            PostalCode = int.Parse(PostalCode.Text)
	        };
	        Guid apartmentId = await _registrationService.AddApartment(apartment);
	    }
	}
}