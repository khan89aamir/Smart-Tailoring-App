
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.DAL;
using Smart_Tailoring_Solution_App.Model;
using System.Collections.Generic;

namespace Smart_Tailoring_Solution_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Customer : ContentPage
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void btnNewCustomer_Clicked(object sender, System.EventArgs e)
        {
            pnlCustomerInfo.IsVisible = true;
            pnlExistingCustomer.IsVisible = false;
            btnNextOption.IsVisible = true;
        }

        private void btnOldCustomer_Clicked(object sender, System.EventArgs e)
        {
            pnlCustomerInfo.IsVisible = false;
            pnlExistingCustomer.IsVisible = true;
            btnNextOption.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadCustomer();
            txtCustomerName.Focus();
        }

        private void LoadCustomer()
        {
            List<tblCustomer> tblCustomers = DAL.TAILORING_DB.Instance.GetNonSyncCustomer();
            dgvCustomer.ItemsSource = TAILORING_DB.Instance.GetCustomerAsync();
        }

        void Button_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Measurement());
        }

        private void btnNext_Clicked(object sender, System.EventArgs e)
        {
            string strCustomerName = txtCustomerName.Text == null ? "" : txtCustomerName.Text.Trim();
            string strMobileNo = txtMobileNo.Text == null ? "" : txtMobileNo.Text.Trim();
            string strAddress = txtAddress.Text == null ? "" : txtAddress.Text.Trim();
            if (strCustomerName.Trim().Length == 0)
            {
                DisplayAlert("Customer", "Please Enter Name.", "Ok");
                txtCustomerName.Focus();
                return;
            }
            if (strMobileNo.Trim().Length == 0)
            {
                DisplayAlert("Customer", "Please Enter Mobile No.", "Ok");
                txtMobileNo.Focus();
                return;
            }
            //if (txtMail.Text.Trim().Length == 0)
            //{
            //    DisplayAlert("Customer", "Please Enter Email ID.", "Ok");
            //    txtMail.Focus();
            //    return;
            //}
            if (strAddress.Trim().Length == 0)
            {
                DisplayAlert("Customer", "Please Enter Address.", "Ok");
                txtAddress.Focus();
                return;
            }
            int cnt = DAL.TAILORING_DB.Instance.DuplicateMobileNo(strMobileNo.Trim());
            if (cnt == 0)
            {
                tblCustomer ObjCustomer = new tblCustomer();
                ObjCustomer.CustomerID = 0;
                ObjCustomer.Address = strAddress;
                ObjCustomer.EmailID = txtMail.Text;
                ObjCustomer.Name = strCustomerName;
                ObjCustomer.MobileNo = strMobileNo;
                ObjCustomer.LastChange = 0;

                int result = TAILORING_DB.Instance.SaveCustomer(ObjCustomer);
                if (result > 0)
                {
                    DisplayAlert("Customer", "Customer have been added.", "Ok");
                    ClearAll();
                }
            }
            else
            {
                DisplayAlert("Customer", "Duplicate Customer Mobile No ['" + txtMobileNo.Text + "]'", "Ok");
            }
        }

        private void btnDelete_Clicked(object sender, System.EventArgs e)
        {
            dgvCustomer.IsRefreshing = true;

            Button button = (Button)sender;

            // this will give you all info 
            tblCustomer _cust = (tblCustomer)button.BindingContext;

            TAILORING_DB.Instance.DeleteCustomerAsync(_cust);

            dgvCustomer.ItemsSource = null;
            LoadCustomer();
            dgvCustomer.IsRefreshing = false;
            return;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchMobileNo.Text.Trim().Length == 0)
            {
                LoadCustomer();
            }
            else
            {
                dgvCustomer.ItemsSource = TAILORING_DB.Instance.GetCustoemrByMobile(txtSearchMobileNo.Text);
            }
        }

        private void txtSearchCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchCustomerName.Text.Trim().Length == 0)
            {
                LoadCustomer();
            }
            else
            {
                dgvCustomer.ItemsSource = TAILORING_DB.Instance.GetCustoemrByName(txtSearchCustomerName.Text);
            }
        }
        ViewCell lastCell;
        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#E8E8E8");
                lastCell = viewCell;
            }
            Navigation.PushAsync(new Measurement());
        }

        private void btnEdit_Clicked(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            var binding = button.BindingContext;

            tblCustomer tc = (tblCustomer)(binding);

            Navigation.PushAsync(new View.frmCustomerEdit(tc));
        }

        private void ClearAll()
        {
            txtCustomerName.Text = "";
            txtAddress.Text = "";
            txtMail.Text = "";
            txtMobileNo.Text = "";

            txtCustomerName.Focus();
        }
    }
}