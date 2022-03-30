using Android.Content;
using MapSocNetXamarin.Droid;
using Xamarin.Forms;
using MapSocNetXamarin.Models;
using System.Threading.Tasks;
using System.IO;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace MapSocNetXamarin.Droid
{

    public class PhotoPickerService : IDeviceDataLoader
    {
        public async Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return await MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}
