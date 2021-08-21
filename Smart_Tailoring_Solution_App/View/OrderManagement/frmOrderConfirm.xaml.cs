using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Net.Http;
using Smart_Tailoring_Solution_App.Model;

namespace Smart_Tailoring_Solution_App.View.OrderManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmOrderConfirm : ContentPage
    {
        public frmOrderConfirm()
        {
            InitializeComponent();

            Utility.GarmentID = 0;
            Utility.MasterGarmentID = 0;
        }

        string URL = string.Empty;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            URL = "http://" + Service.clsSmartTailoringService.ServerIPAddress + "//temp/";
        }

        private void btnCustomerCopy_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.Browser.OpenAsync("http://192.168.43.51//temp/Customer.pdf");
            Xamarin.Essentials.Browser.OpenAsync(URL + "Customer.pdf");
        }

        private void btnStoreCopy_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.Browser.OpenAsync("http://192.168.43.51//temp/Store.pdf");
            Xamarin.Essentials.Browser.OpenAsync(URL + "Store.pdf");
        }

        private void btnFactoryCopy_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.Browser.OpenAsync("http://192.168.43.51//temp/factory.pdf");
            Xamarin.Essentials.Browser.OpenAsync(URL + "factory.pdf");
        }
    }
}