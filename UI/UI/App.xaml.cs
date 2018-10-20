using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UI.Services;
using UI.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UI
{
    public partial class App : Application
    {
        public static string AzureBackendUrl = "https://rowhouseturnmanagementapi.azurewebsites.net";

        public App()
        {
            InitializeComponent();

            DependencyService.Register<AzureDataStore>();

            MainPage = new MainPage();
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
