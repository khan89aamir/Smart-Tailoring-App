using Smart_Tailoring_Solution_App.Service;
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
    public partial class frmSelectSKU : ContentPage
    {
        public frmSelectSKU()
        {
            InitializeComponent();

            lstGarmnetList = new ObservableCollection<Model.GarmentList>();
            lstGarmnetList.Add(new Model.GarmentList
            {
                GarmentID = 1,
                Name = "Shirt",

                ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Bandgalan1.png"
            }); ; ;
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 2, Name = "Trouser", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Trouser%20Generic%201.png" });
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 3, Name = "2-PC Suit", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 4, Name = "Jacket", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 5, Name = "3-PC Suit", ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });



            dgvGarmnetList.ItemsSource = lstGarmnetList;
        }
        ObservableCollection<Model.GarmentList> lstGarmnetList;

        private void dgvGarmnetList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new View.OrderManagement.frmMeasurment());
        }
        ViewCell lastCell;
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
            Navigation.PushAsync(new View.OrderManagement.frmOrderConfirm());

        }
    }
}