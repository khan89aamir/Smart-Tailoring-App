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
            clsCommon cls = new clsCommon();

            List<UserManagement> tblUser = TAILORING_DB.Instance.GetAllUsers();
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
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            txtPassword.IsReadOnly = true;
        }
    }
}