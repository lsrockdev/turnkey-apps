using RightPath.Views;
using Xamarin.Forms;

namespace RightPath
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            //MainPage = new NavigationPage(new MainPage("Start")) { BarBackgroundColor = Color.FromHex("#FFC107"), BarTextColor = Color.White }; //Turnkey
			MainPage = new NavigationPage(new MainPage("Start")) { BarBackgroundColor = Color.FromHex("#AFCB40"), BarTextColor = Color.Black }; //Right path
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
