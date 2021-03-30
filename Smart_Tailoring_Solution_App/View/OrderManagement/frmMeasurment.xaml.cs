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
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            tabBodyPsoture.BackgroundColor = Color.FromHex("#0091D5");

            tabMeasure.BackgroundColor = Color.FromHex("#525B72");
            tabStyle.BackgroundColor = Color.FromHex("#525B72");


            pnlBodyPosture.IsVisible = true;
            pnlMeasur.IsVisible = false;
            pnlStyle.IsVisible = false;
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
            Service.clsSmartTailoringService ObjService = new Service.clsSmartTailoringService();
            var MeasurmentData = await ObjService.GetGarmentMasterMeasurement(1);
            dgvMeasurment.ItemsSource = MeasurmentData;

            pickerStitchType.ItemsSource = await ObjService.GetStitchType();
            pickerFitType.ItemsSource = await ObjService.GetFitType();


            //Testing multiple-complex-type-parameters
            //Model.OrderModel.clsMeasurment measure = new Model.OrderModel.clsMeasurment();
            //measure.MeasurmentID = 1;
            //measure.MeasurmentName = "Length";
            //measure.value = "1";

            //Model.OrderModel.clsStyle style = new Model.OrderModel.clsStyle();
            //style.StyleID = 1;
            //style.StyleName = "Collor";
            //style.IsMandatory = true;

            //var result= await ObjService.SetMeasurementStyle(measure, style);
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
            Navigation.PushAsync(new View.OrderManagement.frmOrderConfirm());
        }
    }
}