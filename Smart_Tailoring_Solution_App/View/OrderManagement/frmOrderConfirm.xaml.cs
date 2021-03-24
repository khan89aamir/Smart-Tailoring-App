﻿using System;
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

        string URL = string.Empty;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            URL = "http://" + Service.clsSmartTailoringService.ServerIPAddress + "//Smart_Tailoring_WebAPI/temp/";
        }

        private void btnCustomerCopy_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.Browser.OpenAsync("http://192.168.0.6:3720//Smart_Tailoring_WebAPI/temp/Customer.pdf");
            Xamarin.Essentials.Browser.OpenAsync(URL + "Customer.pdf");
        }

        private void btnStoreCopy_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.Browser.OpenAsync("http://192.168.0.6:3720//Smart_Tailoring_WebAPI/temp/Store.pdf");
            Xamarin.Essentials.Browser.OpenAsync(URL + "Store.pdf");
        }

        private void btnFactoryCopy_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.Browser.OpenAsync("http://192.168.0.6:3720//Smart_Tailoring_WebAPI/temp/factory.pdf");
            Xamarin.Essentials.Browser.OpenAsync(URL + "factory.pdf");
        }
    }
}