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
    public partial class frmOrderPage2 : ContentPage
    {
        public List<SelectedGarments> lstSelectedGarment = new List<SelectedGarments>();
        clsSmartTailoringService service = new clsSmartTailoringService();

        public frmOrderPage2()
        {
            InitializeComponent();

            lblTailroingamt.Text = "TailoringAmt : 0";
            lblTrimAmount.Text = "Trim Amount : 0";
            lblTax.Text = "Tax : 0";
            lblGrandTotal.Text = "Grand Total : 0";
        }

        private void ShowHideLoading(bool b)
        {
            ctrActivityIndicator.IsVisible = b;
            stGarments.IsVisible = !b;
        }

        private async void BindSelectedGarmentRate()
        {
            lstSelectedGarment = await service.GetGarmentRateListBySelectedGarment(lstSelectedGarment);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowHideLoading(true);

            BindSelectedGarmentRate();
            lstDefaultBindOrderType();

            ShowHideLoading(false);

            dgvAddedGarmnet.ItemsSource = lstSelectedGarment;
        }

        private void lstDefaultBindOrderType()
        {
            for (int i = 0; i < lstSelectedGarment.Count; i++)
            {
                SelectedGarments item = lstSelectedGarment[i];

                // Set the OrderType to the selected Product.
                //item.OrderTypeID = 0;

                if (lstSelectedGarment[i].lstGarmentRate != null && lstSelectedGarment[i].lstGarmentRate.Count > 0)
                {
                    var lst = lstSelectedGarment[i].lstGarmentRate.Where(g => g.GarmentID == item.GarmentID && g.OrderTypeID == 0).First();

                    item.OrderTypeID = lst.OrderTypeID;
                    item.OrderType = lst.OrderType;
                    item.Rate = lst.Rate;
                    item.GarmentCode = lst.GarmentCode;
                }
            }
        }

        private void chkTrailDate_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            frmSelectSKU frmSKU = new frmSelectSKU();
            frmSKU.lstSelectedGarmnetList = lstSelectedGarment;
            await Navigation.PushAsync(frmSKU);
        }

        private void cmbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // get the selected garment Item
                SelectedGarments garmentItem = ((Picker)sender).BindingContext as SelectedGarments;

                // get the select UOM item
                Picker cmbService = ((Picker)sender);
                var selectedItem = cmbService.SelectedItem as Model.OrderModel.clsGarmentRate;

                if (selectedItem == null)
                    return;
                // Set the Service to the selected Garment.
                garmentItem.OrderType = selectedItem.OrderType;
                garmentItem.OrderTypeID = selectedItem.OrderTypeID;

                var lst = garmentItem.lstGarmentRate.Where(g => g.GarmentID == garmentItem.GarmentID && g.OrderTypeID == selectedItem.OrderTypeID).First();

                garmentItem.Rate = lst.Rate;

                Calculate_Garment_COST();
            }
            catch (Exception ex)
            {
                DisplayAlert(ex.Message, ex.ToString(), "Ok");
            }
        }

        private void Calculate_Garment_COST()
        {
            double TailoringAmt = 0, TrimAmt = 0, Tax = 0, TotalAmt = 0;

            TailoringAmt = lstSelectedGarment.Sum<SelectedGarments>(g => g.Rate * g.QTY);
            TrimAmt = lstSelectedGarment.Sum<SelectedGarments>(g => g.TrimAmount);
            TotalAmt = TailoringAmt + TrimAmt + Tax;

            lblTailroingamt.Text = "TailoringAmt : " + TailoringAmt.ToString();
            lblTrimAmount.Text = "Trim Amount : " + TrimAmt.ToString();
            lblTax.Text = "Tax : " + Tax.ToString();
            lblGrandTotal.Text = "Grand Total : " + TotalAmt.ToString();
        }

        private void txtTrimAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null && e.NewTextValue.Length > 0)
            {
                // get the selected garment Item
                SelectedGarments garmentItem = ((Entry)sender).BindingContext as SelectedGarments;
                garmentItem.TrimAmount = Convert.ToDouble(e.NewTextValue);
                Calculate_Garment_COST();
            }
        }
    }
}