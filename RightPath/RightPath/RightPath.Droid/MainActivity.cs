using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace RightPath.Droid
{
    [Activity(Label = "RPRE Estimates", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            //base.SetTheme(Resource.Style.Theme_AppCompat);

            // set the layout resources first
            //FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            //FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}