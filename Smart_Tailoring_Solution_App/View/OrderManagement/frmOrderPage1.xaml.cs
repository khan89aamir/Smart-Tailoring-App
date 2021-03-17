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
    public partial class frmOrderPage1 : ContentPage
    {
        ObservableCollection<Model.GarmentList> lstGarmnetList;

        public frmOrderPage1()
        {
            InitializeComponent();

            
            
            lstGarmnetList = new ObservableCollection<Model.GarmentList>();
          
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 1, Name = "Shirt", DeliveryDate = DateTime.Now, QTY = 1, TrailDate = DateTime.Now, ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress+ "/Images/Bandgalan1.png"
            });;
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 2, Name = "Trouser", DeliveryDate = DateTime.Now, QTY = 1, TrailDate = DateTime.Now, ImageURL ="http://"+ clsSmartTailoringService.ServerIPAddress + "/Images/Trouser%20Generic%201.png" });
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 3, Name = "2-PC Suit", DeliveryDate = DateTime.Now, QTY = 1, TrailDate = DateTime.Now, ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 4, Name = "Jacket", DeliveryDate = DateTime.Now, QTY = 1, TrailDate = DateTime.Now, ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });
            lstGarmnetList.Add(new Model.GarmentList { GarmentID = 5, Name = "3-PC Suit", DeliveryDate = DateTime.Now, QTY = 1, TrailDate = DateTime.Now, ImageURL = "http://" + clsSmartTailoringService.ServerIPAddress + "/Images/Shirt%20generic%201.png" });



            dgvGarmnetList.ItemsSource = lstGarmnetList;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pnlGarmentDetails.IsVisible)
            {
                pnlGarmentDetails.IsVisible = false;
            }
            if (!dgvGarmnetList.IsVisible)
            {
                dgvGarmnetList.IsVisible = true;
            }
            ////thats all you need to make a search  
            IList<Model.GarmentList> data = lstGarmnetList.Where(p =>
                       p.Name != null &&
                       p.Name.ToLower().StartsWith(e.NewTextValue.ToLower())
                       ).ToList();

            int count = data.Count;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {



                dgvGarmnetList.ItemsSource = data;
            }

            else
            {
                ObservableCollection<Model.GarmentList> myCollection = new ObservableCollection<Model.GarmentList>(data);

                dgvGarmnetList.ItemsSource = myCollection;
            }

        }

        private void dgvGarmnetList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var SelecteGarment=   e.Item as Model.GarmentList;
            dgvGarmnetList.IsVisible = false;
            pnlGarmentDetails.IsVisible = true;
            txtGarmentName.Text = SelecteGarment.Name;
            picQTY.SelectedIndex=0;
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

        private void dgvGarmnetList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        ObservableCollection<Model.GarmentList> lstSelectedGarment = new ObservableCollection<Model.GarmentList>();
        private void btnAdd_Clicked(object sender, EventArgs e)
        {
           
            for (int i = 1; i <= Convert.ToInt32(picQTY.SelectedItem); i++)
            {
                Model.GarmentList garment = new Model.GarmentList();
                garment.Name = txtGarmentName.Text;
                garment.QTY = 1;

                lstSelectedGarment.Add(garment);
            }

            dgvAddedGarmnet.ItemsSource = lstSelectedGarment;
        }
    }
}