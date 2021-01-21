
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.DAL;
using Smart_Tailoring_Solution_App.Model;

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
        protected override  void OnAppearing()
        {
            base.OnAppearing();

            LoadCustomer();


        }
        private  void LoadCustomer()
        {
            dgvCustomer.ItemsSource =  TAILORING_DB.Instance.GetCustomerAsync();
        }
         void Button_Clicked(object sender, System.EventArgs e)
        {

         

            App.Current.MainPage = new NavigationPage(new Measurement());



        }

        private  void btnNext_Clicked(object sender, System.EventArgs e)
        {

            tblCustomer ObjCustomer = new tblCustomer();
            ObjCustomer.CustomerID = 1;
            ObjCustomer.Address = txtAddress.Text;
            ObjCustomer.EmailID = txtMail.Text;
            ObjCustomer.Name = txtCustomerName.Text;
            ObjCustomer.MobileNo = txtMobileNo.Text;

           int result=  TAILORING_DB.Instance.SaveCustomer(ObjCustomer);
            if (result>0)
            {
                DisplayAlert("Customer", "Customer have been added.","Ok");
            }
        }

        private  void btnDelete_Clicked(object sender, System.EventArgs e)
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
            if (txtSearchMobileNo.Text.Trim().Length==0)
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
            var binding= button.BindingContext;

            tblCustomer tc = (tblCustomer)(binding);
            

            Navigation.PushAsync(new View.frmCustomerEdit(tc));
        }
    }

}