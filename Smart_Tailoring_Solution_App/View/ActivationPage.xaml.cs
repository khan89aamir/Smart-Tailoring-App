using Android.OS;
using System;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.DAL;
using Smart_Tailoring_Solution_App.Model;
using Smart_Tailoring_Solution_App.Service;
using static Android.Provider.Settings;

namespace Smart_Tailoring_Solution_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivationPage : ContentPage
    {
        public ActivationPage()
        {
            InitializeComponent();
            var strText = Id;
            lblSerialNo.Text = strText.ToUpper();
        }

        clsSmartTailoringService ObjService = new clsSmartTailoringService();

        static string id = string.Empty;
        public static string Id
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(id))
                    return id;

                id = Android.OS.Build.Serial;
                if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
                {
                    try
                    {
                        var context = Android.App.Application.Context;
                        id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                    }
                    catch (Exception ex)
                    {

                        Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                    }
                }

                return id;
            }
        }
        // activea button
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (txtServerIP.Text.Trim().Length == 0)
            {
                await DisplayAlert("Server IP", "Please Enter Server IP Address", "OK");
                txtServerIP.Focus();
                return;
            }
            ctrActivityIndicator.IsVisible = true;
            var conResult = await ObjService.CheckServerConnection(txtServerIP.Text);
            if (conResult)
            {
                var result = await ObjService.ValidateActivation(lblSerialNo.Text, txtActivationCode.Text, txtServerIP.Text);
                if (result)
                {
                    SaveActivationDetails();
                    ctrActivityIndicator.IsVisible = false;
                    await DisplayAlert("Activation", "Application has been activated !", "Ok");

                    App.Current.MainPage = new View.frmLogin();
                }
                else
                {
                    ctrActivityIndicator.IsVisible = false;
                    await DisplayAlert("Activation Error", "Failed to activate the application.Pleae check the activation code.", "Ok");
                }
            }
            else
            {
                ctrActivityIndicator.IsVisible = false;
                await DisplayAlert("Activation Error", "Unable to connect to the server. Check the server IP address.", "Ok");
            }
        }

        private async void btnConnectToServer_Clicked(object sender, EventArgs e)
        {
            if (txtServerIP.Text.Trim().Length == 0)
            {
                await DisplayAlert("Server IP", "Please Enter Server IP Address", "OK");
                txtServerIP.Focus();
                return;
            }
            ctrActivityIndicator.IsVisible = true;

            // check if th service is available. 
            var result = await ObjService.SendActivationRequest(txtServerIP.Text, lblSerialNo.Text);

            if (result)
            {
                ctrActivityIndicator.IsVisible = false;
                await DisplayAlert("Server Connection", "Connection has been established. Please enter activation Code.", "Ok");
            }
            else
            {
                
                ctrActivityIndicator.IsVisible = false;
                await DisplayAlert("Server Connection Failed", "Not able to connect to the server. Please check the server ip address.", "Ok");
            }
        }

        private bool SaveActivationDetails()
        {
            // save the data into database

            ActivationDetails activation = new ActivationDetails();
            activation.ActivationCode = txtActivationCode.Text;
            activation.DeviceSerialNumber = lblSerialNo.Text;
            activation.ServerIP = txtServerIP.Text;

            int result = TAILORING_DB.Instance.SaveActivationDetails(activation);
            if (result > 0)
            {
                clsSmartTailoringService.ServerIPAddress = txtServerIP.Text;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}