using Smart_Tailoring_Solution_App.Service;
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
    public partial class frmEmployee : ContentPage
    {
        public frmEmployee()
        {
            InitializeComponent();

            LoadEmployee();
        }
        clsSmartTailoringService ObjService = new clsSmartTailoringService();

        private async void LoadEmployee()
        {
            var lstEmployeeDetails = await ObjService.GetEmployeeDetails(clsSmartTailoringService.UserID);
            dgvEmployee.ItemsSource = lstEmployeeDetails;
        }
    }
}