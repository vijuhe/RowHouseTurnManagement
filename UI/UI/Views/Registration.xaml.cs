using System;
using System.Text.RegularExpressions;
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
	        try
            {
                Guid apartmentId = await _registrationService.AddApartment(apartment);
                KeyValueStorage.SetApartmentId(apartmentId);
            }
            catch (Exception)
            {
                ErrorMessage.Text = "Rekisteröinti epäonnistui. Ota yhteyttä sovelluksen ylläpitoon.";
            }
        }

        private void CheckFormValidity(object sender, TextChangedEventArgs e)
	    {
	        bool formValid = AllFieldsAreFilled() && StreetAddressIsValid() && PostalCodeIsValid();
	        if (formValid)
	        {
	            ErrorMessage.Text = string.Empty;
	        }
	        else if (AllFieldsAreFilled())
	        {
	            if (!StreetAddressIsValid())
	            {
	                ErrorMessage.Text = "Katuosoitteen pitää päättyä asunnon numeroon.";
	            }
	            else if (!PostalCodeIsValid() && PostalCode.Text.Length == PostalCode.MaxLength)
	            {
	                ErrorMessage.Text = "Postinumero muodostuu viidestä numerosta.";
	            }
	        }
	        Register.IsEnabled = formValid;
	    }

	    private bool StreetAddressIsValid()
	    {
	        return Regex.IsMatch(StreetAddress.Text, @"\d+\s*$");
	    }

	    private bool PostalCodeIsValid()
	    {
	        return Regex.IsMatch(PostalCode.Text, @"\d{5}");
	    }

	    private bool AllFieldsAreFilled()
	    {
	        return LastName.Text?.Length > 0 && StreetAddress.Text?.Length > 0 && PostalCode.Text?.Length > 0;
	    }
	}
}