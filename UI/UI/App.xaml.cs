using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UI.Services;
using UI.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UI
{
    public partial class App : Application
    {
        //public static Uri BackendUrl = new Uri("https://rowhouseturnmanagementapi.azurewebsites.net/api/");
        public static Uri BackendUrl = new Uri("http://localhost:5000/api/");
        public static Guid BackendApiKey = Guid.Parse("CC685128-F9F9-484E-A324-047F5D2F95BE");

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IRegistrationService, RegistrationService>();

            MainPage = new Registration(new RegistrationService());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
