using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;

namespace RightPath.Droid
{
    [Activity(Label = "RPRE Estimates", Icon = "@drawable/icon", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            var startUp = new Task(() =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
            startUp.ContinueWith(t => Finish());

            startUp.Start();
        }
    }
}