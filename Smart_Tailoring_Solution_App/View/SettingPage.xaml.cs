
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.DAL;
using Smart_Tailoring_Solution_App.MenuItems;

namespace Smart_Tailoring_Solution_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
   

        public SettingPage()
        {
            InitializeComponent();

      
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadActivationDetails();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

            if(DAL.TAILORING_DB.Instance.DeleteActivation()>0)
            {
                DisplayAlert("Smart Tailoring", "Application Deactivated", "ok");
                Model.Utility.WriteLog("Application Deactivated");
            }

          

        }
        private void LoadActivationDetails()
        {
            List<Model.ActivationDetails> lstActivationDetails = TAILORING_DB.Instance.GetActivationDetails();
            if (lstActivationDetails.Count>0)
            {
                txtActivationCode.Text = lstActivationDetails[0].ActivationCode;
                txtSerialNo.Text = lstActivationDetails[0].DeviceSerialNumber;
                txtServerIP.Text= lstActivationDetails[0].ServerIP;
                
            }
        }
    }
}