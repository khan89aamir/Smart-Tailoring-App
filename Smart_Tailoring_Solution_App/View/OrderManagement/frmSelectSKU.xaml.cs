using Smart_Tailoring_Solution_App.Model;
using Smart_Tailoring_Solution_App.Service;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.View.OrderManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmSelectSKU : ContentPage
    {
        ObservableCollection<Model.GarmentList> lstGarmnetList;
        ViewCell lastCell;

        public frmSelectSKU()
        {
            InitializeComponent();

            lstGarmnetList = new ObservableCollection<Model.GarmentList>();
            lstGarmnetList.Add(new GarmentList
            {
                GarmentID = 1,
                Name = "Shirt",

                ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Bandgalan1.png"
            }); ; ;
            lstGarmnetList.Add(new GarmentList { GarmentID = 2, Name = "Trouser", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Trouser%20Generic%201.png" });
            lstGarmnetList.Add(new GarmentList { GarmentID = 3, Name = "2-PC Suit", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });
            lstGarmnetList.Add(new GarmentList { GarmentID = 4, Name = "Jacket", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });
            lstGarmnetList.Add(new GarmentList { GarmentID = 5, Name = "3-PC Suit", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });


            dgvGarmnetList.ItemsSource = lstGarmnetList;
        }


        private void dgvGarmnetList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            GarmentList Garlst = (GarmentList)e.Item;
            Utility.GarmentID = Garlst.GarmentID;
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