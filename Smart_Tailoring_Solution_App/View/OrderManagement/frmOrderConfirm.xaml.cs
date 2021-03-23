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
namespace Smart_Tailoring_Solution_App.View.OrderManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmOrderConfirm : ContentPage
    {
        public frmOrderConfirm()
        {
            InitializeComponent();

           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
          

          
        }
      

        private void Button_Clicked(object sender, EventArgs e)
        {
            Xamarin.Essentials.Browser.OpenAsync("http://192.168.43.63//Smart_Tailoring_WebAPI/temp/Customer.pdf");
          
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Xamarin.Essentials.Browser.OpenAsync("http://192.168.43.63//Smart_Tailoring_WebAPI/temp/Store.pdf");
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
                Xamarin.Essentials.Browser.OpenAsync("http://192.168.43.63//Smart_Tailoring_WebAPI/temp/factory.pdf");
            

        }
    }
}