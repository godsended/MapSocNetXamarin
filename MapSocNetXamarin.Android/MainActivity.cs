using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Android.Content;
using FFImageLoading.Forms.Platform;
using MapSocNetXamarin.Models;
using Xamarin.Forms.Platform.Android;

namespace MapSocNetXamarin.Droid
{
    [Activity(Label = "Bumber", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            Instance = this;
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("UseLegacyRenderers");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Settings.ThemeChanged += OnThemeChanged;
            OnThemeChanged(null, null);

            ImageCircleRenderer.Init();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Color color = (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["CurrentMainColor"];

            SetStatusBarColor(color.ToAndroid());
        }
    }
}