using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmCustomerEdit : ContentPage
    {
        int MB_CustomerID = 0;
        int CustomerID = 0;
        public frmCustomerEdit(Model.tblCustomer tblCustomer)
        {
            InitializeComponent();

            txtCustomerName.Text = tblCustomer.Name;
            txtAddress.Text = tblCustomer.Address;
            txtMail.Text = tblCustomer.EmailID;
            txtMobileNo.Text = tblCustomer.MobileNo;
            MB_CustomerID = tblCustomer.MB_CustomerID;
            this.CustomerID = tblCustomer.CustomerID;
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            if (Validation())
            {
                UpdateCustomerData();
            }
        }
        private void UpdateCustomerData()
        {
            try
            {
                Model.tblCustomer customer = new Model.tblCustomer();
                customer.Address = txtAddress.Text;
                customer.CustomerID = this.CustomerID;
                customer.EmailID = txtMail.Text;
                customer.MobileNo = txtMobileNo.Text;
                customer.Name = txtCustomerName.Text;
                customer.MB_CustomerID = this.MB_CustomerID;
                customer.LastChange = 0; // it means the data will be pushed back to the server.

                int result = DAL.TAILORING_DB.Instance.UpdateCustomer(customer);
                if (result > 0)
                {
                    Model.Utility.WriteLog("Customer data has been updated");

                    List<Model.tblCustomer> lstCustomer = new List<Model.tblCustomer>();
                    lstCustomer.Add(customer);

                    Model.clsSyncOperation clsSyncOperation = new Model.clsSyncOperation();
                    clsSyncOperation.SyncCustomerData(lstCustomer, true);

                    Model.Utility.ShowMessageBox("Customer data has been updated in device.", Model.Utility.MessageType.Success, this);

                    Navigation.PushAsync(new Customer());
                }
            }
            catch (Exception ex)
            {
                Model.Utility.WriteLog("UpdateCustomerData : " + ex.ToString());
            }

        }
        private bool Validation()
        {
            if (txtCustomerName.Text.Trim().Length == 0)
            {
                Model.Utility.ShowMessageBox("Please Enter Customer Name.", this);
                return false;
            }
            if (txtAddress.Text.Trim().Length == 0)
            {
                Model.Utility.ShowMessageBox("Please Enter Customer Address.", this);
                return false;
            }
            if (txtMobileNo.Text.Trim().Length == 0)
            {
                Model.Utility.ShowMessageBox("Please Enter Customer Mobile No.", this);
                return false;
            }
            //if (txtMail.Text.Trim().Length == 0)
            //{
            //    Model.Utility.ShowMessageBox("Please Enter Customer EMail.", this);
            //    return false;
            //}
            return true;
        }

        private async void btnDelet_Clicked(object sender, EventArgs e)
        {
            bool result = await Model.Utility.ShowQuestionMessageBox("Are you sure, you want to delete this customer?", this);
            if (result)
            {
                int isDelete = DAL.TAILORING_DB.Instance.DeleteCustomerAsync(new Model.tblCustomer { MB_CustomerID = this.MB_CustomerID });
                if (isDelete > 0)
                {
                    Model.Utility.ShowMessageBox("Customer has been deleted successfully.", Model.Utility.MessageType.Success, this);
                    Model.Utility.WriteLog("Custoemr has been deleted from device");

                    await Navigation.PushAsync(new Customer());
                }
            }
        }
    }
}