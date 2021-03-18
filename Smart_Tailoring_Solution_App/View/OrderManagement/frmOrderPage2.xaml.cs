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
        public ObservableCollection<Model.GarmentList> lstSelectedGarment;
        public frmOrderPage2()
        {
            InitializeComponent();

        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            dgvAddedGarmnet.ItemsSource = lstSelectedGarment;

        }

    }
}