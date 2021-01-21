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
    public partial class frmLogs : ContentPage
    {
        public frmLogs()
        {
            InitializeComponent();

            txtLogs.Text = Model.Utility.ReadLogs();

            lblLogSize.Text = Model.Utility.GetLogFileSize()+ " MB";
        }

        private void btnDelete_Clicked(object sender, EventArgs e)
        {
            Model.Utility.DeleteLogFile();

            DisplayAlert("Logs", "Logs deleted successfully.","Ok");

            txtLogs.Text = Model.Utility.ReadLogs();


        }
    }
}