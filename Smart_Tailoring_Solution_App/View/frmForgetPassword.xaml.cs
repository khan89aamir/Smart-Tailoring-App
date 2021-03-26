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
    }
}