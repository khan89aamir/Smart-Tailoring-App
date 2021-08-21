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
                App.Current.MainPage = new frmForgetPassword();
                //Navigation.PushAsync(new frmForgetPassword());
                //Navigation.PushAsync(new NavigationPage(new frmForgetPassword()));
            };
            lblForgotPassword.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            string strUserName = txtUserName.Text == null ? "" : txtUserName.Text.Trim();
            string strPassword = txtPassword.Text == null ? "" : txtPassword.Text.Trim();

            var conResult = await ObjService.CheckServerConnection(clsSmartTailoringService.ServerIPAddress);
            if (conResult)
            {
                bool IsforceLogin = false;

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
                    ctrActivityIndicator.IsVisible = true;
                    var conStatus = await ObjService.ValidateLogin(strUserName, strPassword, clsSmartTailoringService.ServerIPAddress);
                    if (conStatus)
                    {
                        ctrActivityIndicator.IsVisible = false;
                        App.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        ctrActivityIndicator.IsVisible = false;
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
                ctrActivityIndicator.IsVisible = true;
                var conStatus = DAL.TAILORING_DB.Instance.ValidateLocalLogin(strUserName.Trim(), strPassword.Trim());
                if (conStatus > 0)
                {
                    ctrActivityIndicator.IsVisible = false;
                    App.Current.MainPage = new MainPage();
                }
                else
                {
                    ctrActivityIndicator.IsVisible = false;
                    await DisplayAlert("Server Connection Failed", "Not able to connect to the server. Please check the server ip address.", "Ok");
                }
            }
        }
    }
}