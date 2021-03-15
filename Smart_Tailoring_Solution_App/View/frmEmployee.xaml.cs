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
        public frmEmployee()
        {
            InitializeComponent();

            LoadEmployee();
            LoadUsers();
        }
        clsSmartTailoringService ObjService = new clsSmartTailoringService();

        private async void LoadEmployee()
        {
            var lstEmployeeDetails = await ObjService.GetEmployeeDetails(clsSmartTailoringService.UserID);
            dgvEmployee.ItemsSource = lstEmployeeDetails;
        }
        private void LoadUsers()
        {
            clsCommon cls = new clsCommon();

            var lstUserDetails = TAILORING_DB.Instance.GetAllUsers();
            dgvUsers.ItemsSource = lstUserDetails;
        }
    }
}