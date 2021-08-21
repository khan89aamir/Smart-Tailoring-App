using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.View.OrderManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmOrderPage2 : ContentPage
    {
        public ObservableCollection<Model.SelectedGarments> lstSelectedGarment;
        public frmOrderPage2()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            dgvAddedGarmnet.ItemsSource = lstSelectedGarment;

            var grid = dgvAddedGarmnet.ItemsSource;
        }

        private void chkTrailDate_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
               
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new View.OrderManagement.frmSelectSKU());
        }
    }
}