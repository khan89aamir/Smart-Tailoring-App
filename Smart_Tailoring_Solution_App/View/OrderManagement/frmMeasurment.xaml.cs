using Smart_Tailoring_Solution_App.Model;
using Smart_Tailoring_Solution_App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.View.OrderManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmMeasurment : ContentPage
    {
        public frmMeasurment()
        {
            InitializeComponent();
            ShowBodyPosture();
            lblSelectedSKU.Text = "Selected SKU : " + Utility.GarmentName;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ShowBodyPosture();
        }
        private void ShowBodyPosture()
        {
            tabBodyPsoture.BackgroundColor = Color.FromHex("#0091D5");

            tabMeasure.BackgroundColor = Color.FromHex("#525B72");
            tabStyle.BackgroundColor = Color.FromHex("#525B72");


            pnlBodyPosture.IsVisible = true;
            pnlMeasur.IsVisible = false;
            pnlStyle.IsVisible = false;

            LoadBodyPostureImages();

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            tabMeasure.BackgroundColor = Color.FromHex("#0091D5");

            tabBodyPsoture.BackgroundColor = Color.FromHex("#525B72");
            tabStyle.BackgroundColor = Color.FromHex("#525B72");


            pnlBodyPosture.IsVisible = false;
            pnlMeasur.IsVisible = true;
            pnlStyle.IsVisible = false;

            LoadMeasureent();
        }

        private async void LoadMeasureent()
        {
            pnlLoading.IsVisible = true;

            Service.clsSmartTailoringService ObjService = new Service.clsSmartTailoringService();
            var MeasurmentData = await ObjService.GetGarmentMasterMeasurement(Utility.GarmentID);
            dgvMeasurment.ItemsSource = MeasurmentData;

            pnlLoading.IsVisible = false;
        }

        private async void LoadBodyPostureImages()
        {
            List<Model.OrderModel.clsBodyPosture> lstBodyPostureData = new List<Model.OrderModel.clsBodyPosture>();
            clsSmartTailoringService ObjService = new clsSmartTailoringService();
            List<Model.OrderModel.clsBodyPosture> BodyPostureData = await ObjService.GetBodyPosture(Utility.GarmentID);

            for (int i = 0; i < BodyPostureData.Count; i++)
            {
                Model.OrderModel.clsBodyPosture lstbody = BodyPostureData[i];

                for (int j = 0; j < lstbody.lstImage.Count; j++)
                {
                    lstbody.lstImage[j].ImageURL = clsSmartTailoringService.Http + clsSmartTailoringService.ServerIPAddress + lstbody.lstImage[j].ImageURL;
                }
                lstBodyPostureData.Add(lstbody);
            }

            collstBody.ItemsSource = lstBodyPostureData;
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            tabStyle.BackgroundColor = Color.FromHex("#0091D5");

            tabBodyPsoture.BackgroundColor = Color.FromHex("#525B72");
            tabMeasure.BackgroundColor = Color.FromHex("#525B72");


            pnlBodyPosture.IsVisible = false;
            pnlMeasur.IsVisible = false;
            pnlStyle.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void colBody_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TapBodyPosture_Recognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void TapBodyPostureImage_Recognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}