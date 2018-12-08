using UI.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		    if (!KeyValueStorage.HasApartment())
		    {
		        Navigation.PushModalAsync(new Registration(new RegistrationService()), true);
		    }
		}
	}
}