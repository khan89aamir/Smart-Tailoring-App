using Smart_Tailoring_Solution_App.Model;
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
        ObservableCollection<GarmentList> lstGarmnetList;
        List<SelectedGarments> lstSelectedGarment = new List<SelectedGarments>();

        ViewCell lastCell;

        int pGarmentID = 0;
        string strPhoto = string.Empty;
        public frmOrderPage1()
        {
            InitializeComponent();

            lstGarmnetList = new ObservableCollection<GarmentList>();

            LoadGarments();

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

            //thats all you need to make a search
            IList<GarmentList> data = lstGarmnetList.Where(p =>
                       p.Name != null &&
                       p.Name.ToLower().StartsWith(e.NewTextValue.ToLower())
                       ).ToList();

            int count = data.Count;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                dgvGarmnetList.IsVisible = false;
                pnlGarmentDetails.IsVisible = true;
                //dgvGarmnetList.ItemsSource = data;
            }
            else
            {
                ObservableCollection<GarmentList> myCollection = new ObservableCollection<GarmentList>(data);
                dgvGarmnetList.ItemsSource = myCollection;
            }
        }

        private void dgvGarmnetList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var SelecteGarment = e.Item as GarmentList;
            dgvGarmnetList.IsVisible = false;
            pnlGarmentDetails.IsVisible = true;
            txtGarmentName.Text = SelecteGarment.Name;
            picQTY.SelectedIndex = 0;
            pGarmentID = SelecteGarment.GarmentID;
            strPhoto = SelecteGarment.ImageURL;

            Search_GarmentName.Text = "";
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

        private void dgvGarmnetList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            for (int i = 1; i <= Convert.ToInt32(picQTY.SelectedItem); i++)
            {
                SelectedGarments garment = new SelectedGarments();
                garment.Name = txtGarmentName.Text;
                garment.GarmentID = pGarmentID;
                garment.QTY = 1;
                garment.IsTrail = chkTrailDate.IsChecked;
                garment.TrailDate = dtpTrailDate.Date;
                garment.DeliveryDate = dtpDeliveryDate.Date;
                garment.Photo = strPhoto;

                lstSelectedGarment.Add(garment);
            }
            dgvAddedGarmnet.ItemsSource = lstSelectedGarment;
            ClearGarment();
        }

        private void ClearGarment()
        {
            pGarmentID = 0;
            strPhoto = string.Empty;
            txtGarmentName.Text = string.Empty;
            chkTrailDate.IsChecked = false;
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            bool result = await Utility.ShowQuestionMessageBox("Are you sure, you want to delete this Garment?", this);
            if (result)
            {
                Button button = (Button)sender;
                var binding = button.BindingContext;

                SelectedGarments tc = (SelectedGarments)(binding);

                lstSelectedGarment.Remove(tc);

                dgvAddedGarmnet.ItemsSource = lstSelectedGarment;

                Utility.ShowMessageBox("Garment has been deleted successfully.", Utility.MessageType.Success, this);
            }
        }

        private async void LoadGarments()
        {
            clsSmartTailoringService service = new clsSmartTailoringService();
            try
            {
                List<GarmentList> lstGarmnet = await service.GetGarmentList();
                if (lstGarmnet != null)
                {
                    for (int i = 0; i < lstGarmnet.Count; i++)
                    {
                        GarmentList gar = lstGarmnet[i];
                        gar.ImageURL = clsSmartTailoringService.Http + clsSmartTailoringService.ServerIPAddress + gar.ImageURL;

                        lstGarmnetList.Add(gar);
                    }
                }
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                    {
                        { "UserID", clsSmartTailoringService.UserID.ToString() },
                        { "UserName", clsSmartTailoringService.UserName }
                    };
                Microsoft.AppCenter.Crashes.Crashes.TrackError(ex, properties);

                Utility.WriteLog("frmOrderPage1 " + ex.ToString());
            }
        }
        private async void btnMeasurment_Clicked(object sender, EventArgs e)
        {
            if (lstSelectedGarment != null && lstSelectedGarment.Count > 0)
            {
                View.OrderManagement.frmOrderPage2 frmOrderPage2 = new frmOrderPage2();
                frmOrderPage2.lstSelectedGarment = lstSelectedGarment;
                //frmOrderPage2.BindSelectedGarmentRate();
                await Navigation.PushAsync(frmOrderPage2);
            }
            else
            {
                await DisplayAlert(Utility.strMessageTitle, "Please Select Garment", "OK");
            }
        }

        private void chkTrailDate_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (chkTrailDate.IsChecked)
            {
                dtpTrailDate.IsEnabled = true;
            }
            else
            {
                dtpTrailDate.IsEnabled = false;
            }
        }
    }
}