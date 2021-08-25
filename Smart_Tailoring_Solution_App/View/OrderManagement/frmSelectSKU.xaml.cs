using Smart_Tailoring_Solution_App.Model;
using Smart_Tailoring_Solution_App.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.View.OrderManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmSelectSKU : ContentPage
    {
        ObservableCollection<Model.GarmentList> lstGarmnetList;
        public List<SelectedGarments> lstSelectedGarmnetList=new List<SelectedGarments>();
        ViewCell lastCell;

        public frmSelectSKU()
        {
            InitializeComponent();

            lstGarmnetList = new ObservableCollection<GarmentList>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dgvGarmnetList.ItemsSource = lstSelectedGarmnetList;
        }

        private void dgvGarmnetList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //GarmentList Garlst = (GarmentList)e.Item;
            SelectedGarments Garlst = (SelectedGarments)e.Item;
            Utility.GarmentID = Garlst.GarmentID;
            Utility.GarmentName = Garlst.Name;
            Navigation.PushAsync(new frmMeasurment());
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#E8E8E8");
                lastCell = viewCell;
            }
        }

        private void btnMeasurment_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new frmOrderConfirm());
        }
    }
}