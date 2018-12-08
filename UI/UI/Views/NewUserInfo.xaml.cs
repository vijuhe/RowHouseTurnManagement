using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewUserInfo : ContentPage
	{
	    private readonly VisualElement _ownerModal;

	    public NewUserInfo(VisualElement ownerModal)
	    {
	        _ownerModal = ownerModal;
	        InitializeComponent();
	    }

	    private async void CloseDialog(object sender, EventArgs e)
	    {
	        await Navigation.PopModalAsync();
	        await _ownerModal.Navigation.PopModalAsync();
	    }
	}
}