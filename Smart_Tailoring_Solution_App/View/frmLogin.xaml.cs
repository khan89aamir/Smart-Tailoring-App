using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmLogin : ContentPage
    {
        public frmLogin()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {


                Navigation.PushAsync(new frmForgetPassword());
            };

            lblForgotPassword.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage =new  MainPage();
         

        }
    }
}