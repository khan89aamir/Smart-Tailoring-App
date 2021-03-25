using Smart_Tailoring_Solution_App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.DAL;
using Smart_Tailoring_Solution_App.Model;

namespace Smart_Tailoring_Solution_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmEmployee : ContentPage
    {
        clsSmartTailoringService ObjService = new clsSmartTailoringService();
        public frmEmployee()
        {
            InitializeComponent();

            LoadEmployee();
            LoadUsers();
        }
        List<UserManagement> tblUser;
        clsCommon cls = new clsCommon();

        private async void LoadEmployee()
        {
            //var lstEmployeeDetails = await ObjService.GetEmployeeDetails(clsSmartTailoringService.UserID);
            List<Employee> tblEmployee = await ObjService.GetEmployeeDetails(clsSmartTailoringService.UserID);
            if (tblEmployee.Count > 0)
            {
                txtEmployeeCode.Text = tblEmployee[0].EmployeeCode;
                txtEmployeeType.Text = tblEmployee[0].EmployeeType;
                txtEmployeeName.Text = tblEmployee[0].Name;
                txtGender.Text = tblEmployee[0].Gender;
                txtMobileNo.Text = tblEmployee[0].MobileNo;
            }
        }
        private void LoadUsers()
        {
            tblUser = TAILORING_DB.Instance.GetAllUsers();
            tblUser = TAILORING_DB.Instance.GetLoginUser(clsSmartTailoringService.UserID);
            //dgvUsers.ItemsSource = lstUserDetails;
            if (tblUser.Count > 0)
            {
                txtUserName.Text = tblUser[0].UserName;
                txtEmailID.Text = tblUser[0].EmailID;
            }
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            txtPassword.IsReadOnly = false;
            txtPassword.Focus();
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            string strPassword = txtPassword.Text == null ? "" : txtPassword.Text.Trim();
            if (strPassword == "")
            {
                DisplayAlert("Password", "Please Enter New Password", "OK");
                txtPassword.Focus();
                return;
            }
            else
            {
                UserManagement user = new UserManagement();
                user.UserID = tblUser[0].UserID;
                user.UserName = tblUser[0].UserName;
                user.EmailID = tblUser[0].EmailID;
                user.EmployeeID = tblUser[0].EmployeeID;
                user.ActiveStatus = tblUser[0].ActiveStatus;
                user.Password = cls.Encrypt(strPassword, true);
                user.MB_UserID = tblUser[0].MB_UserID;
                int a = TAILORING_DB.Instance.UpdateUserManagement(user);
                if (a > 0)
                {
                    List<Model.UserManagement> lstuser = new List<Model.UserManagement>();
                    lstuser.Add(user);

                    Model.clsSyncOperation clsSyncOperation = new Model.clsSyncOperation();
                    clsSyncOperation.SyncUserManagementData(lstuser, true);

                    Model.Utility.ShowMessageBox("User data has been updated in device.", Model.Utility.MessageType.Success, this);
                }
                txtPassword.IsReadOnly = true;
                txtPassword.Text = "";
            }
        }
    }
}