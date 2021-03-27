using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmForgetPassword : ContentPage
    {
        public frmForgetPassword()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new View.frmLogin();
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new View.frmLogin();
        }

        private async void btnOTP_Clicked(object sender, EventArgs e)
        {
            string strOTP = txtOTP.Text == null ? "" : txtOTP.Text.Trim();
            if (strOTP.Length == 0)
            {
                await DisplayAlert("OTP", "Please Enter OTP", "OK");
                txtOTP.Focus();
                return;
            }
        }

        private async void btnRequestOTP_Clicked(object sender, EventArgs e)
        {
            string strEmailID = txtEmailID.Text == null ? "" : txtEmailID.Text.Trim();
            if (strEmailID.Length == 0)
            {
                await DisplayAlert("Email Address", "Please Enter Email Address", "OK");
                txtEmailID.Focus();
                return;
            }
        }
    }
}