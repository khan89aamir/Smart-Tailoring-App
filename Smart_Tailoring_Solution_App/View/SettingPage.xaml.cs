using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.DAL;
using Smart_Tailoring_Solution_App.MenuItems;
using Smart_Tailoring_Solution_App.Model;
using Smart_Tailoring_Solution_App.Service;

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
            if (DAL.TAILORING_DB.Instance.DeleteActivation() > 0)
            {
                DisplayAlert("Smart Tailoring", "Application Deactivated", "Ok");
                Model.Utility.WriteLog("Application Deactivated");

                //Navigation.PushAsync(new MainPage());
                App.Current.MainPage = new ActivationPage();
            }
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            ActivationDetails activation = new ActivationDetails();

            activation.ActivationCode = txtActivationCode.Text == null ? "" : txtActivationCode.Text.Trim();

            activation.DeviceSerialNumber = txtSerialNo.Text == null ? "" : txtSerialNo.Text.Trim();

            activation.ServerIP = txtServerIP.Text == null ? "" : txtServerIP.Text.Trim();

            if (DAL.TAILORING_DB.Instance.SaveActivationDetails(activation) > 0)
            {
                clsSmartTailoringService.ServerIPAddress = txtServerIP.Text;
                DisplayAlert("Smart Tailoring", "Activation Updated", "Ok");
                Model.Utility.WriteLog("Activation Updated");
            }
        }

        private void LoadActivationDetails()
        {
            List<Model.ActivationDetails> lstActivationDetails = TAILORING_DB.Instance.GetActivationDetails();
            if (lstActivationDetails.Count > 0)
            {
                txtActivationCode.Text = lstActivationDetails[0].ActivationCode;
                txtSerialNo.Text = lstActivationDetails[0].DeviceSerialNumber;
                txtServerIP.Text = lstActivationDetails[0].ServerIP;
            }
        }
    }
}