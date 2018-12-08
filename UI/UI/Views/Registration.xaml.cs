﻿using System;
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
	        Guid apartmentId = await _registrationService.AddApartment(apartment);
            KeyValueStorage.SetApartmentId(apartmentId);
        }

	    private void CheckFormValidity(object sender, TextChangedEventArgs e)
	    {
	        Register.IsEnabled = AllFieldsAreFilled() && PostalCodeIsValid();
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