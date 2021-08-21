using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Smart_Tailoring_Solution_App.Service
{
   public class ImageService
    {
        static readonly HttpClient _client = new HttpClient();

        public static Task<byte[]> DownloadImage(string imageUrl)
        {
          
            return _client.GetByteArrayAsync(imageUrl);
        }

        public static void SaveToDisk(string imageFileName, byte[] imageAsBase64String)
        {
            Xamarin.Essentials.Preferences.Set(imageFileName, Convert.ToBase64String(imageAsBase64String));
        }

        public static Xamarin.Forms.ImageSource GetFromDisk(string imageFileName)
        {
            var imageAsBase64String = Xamarin.Essentials.Preferences.Get(imageFileName, string.Empty);

            return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(imageAsBase64String)));
        }
    }
}
