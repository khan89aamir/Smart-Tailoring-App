using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage1 : ContentPage
    {
        public TestPage1()
        {
            InitializeComponent();
        }
        string xamarinImageName = "XamarinLogo.png";
        private  async void Button_Clicked(object sender, EventArgs e)
        {
            // Get the path to a file on internal storage
            var backingFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "count.txt");

            var imageData=  await Service.ImageService.DownloadImage("http://192.168.43.51/Smart_Tailoring_WebAPI/images/1%20Back%20Pocket.jpg");
          

            Service.ImageService.SaveToDisk(xamarinImageName, imageData);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            picImage.Source = Service.ImageService.GetFromDisk(xamarinImageName);
        }
    }
}