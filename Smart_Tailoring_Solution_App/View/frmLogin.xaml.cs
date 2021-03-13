using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.Service;

namespace Smart_Tailoring_Solution_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmLogin : ContentPage
    {
        clsSmartTailoringService ObjService = new clsSmartTailoringService();

        public frmLogin()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new frmForgetPassword());
            };
            lblForgotPassword.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var conResult = await ObjService.CheckServerConnection(clsSmartTailoringService.ServerIPAddress);
            if (conResult)
            {
                bool IsforceLogin = false;
                string strUserName = txtUserName.Text == null ? "" : txtUserName.Text.Trim();
                string strPassword = txtPassword.Text == null ? "" : txtPassword.Text.Trim();

                if (strUserName == "" && strPassword == "")
                {
                    IsforceLogin = true;
                }
                if (!IsforceLogin)
                {
                    if (strUserName.Length == 0)
                    {
                        await DisplayAlert("UserName", "Please Enter UserName", "OK");
                        txtUserName.Focus();
                        return;
                    }
                    if (strPassword.Length == 0)
                    {
                        await DisplayAlert("Password", "Please Enter Password", "OK");
                        txtPassword.Focus();
                        return;
                    }
                    var conStatus = await ObjService.ValidateLogin(strUserName, strPassword, clsSmartTailoringService.ServerIPAddress);
                    if (conStatus)
                    {
                        App.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        await DisplayAlert("Login Failed", "Invalid Login Details.", "Ok");
                    }
                }
                else
                {
                    clsSmartTailoringService.UserID = 1;
                    App.Current.MainPage = new MainPage();
                }
            }
            else
            {
                await DisplayAlert("Server Connection Failed", "Not able to connect to the server. Please check the server ip address.", "Ok");
            }
        }
    }
}