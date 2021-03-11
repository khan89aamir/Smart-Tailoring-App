using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.DAL;
using Smart_Tailoring_Solution_App.Model;
using Smart_Tailoring_Solution_App.View;

namespace Smart_Tailoring_Solution_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            CheckConectionStatus();
        }

        Service.clsSmartTailoringService ObjService = new Service.clsSmartTailoringService();
        private void Button_Clicked(object sender, EventArgs e)
        {
            pnlHome.IsVisible = true;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var uri = "http://192.168.43.116/api/Masters/Getcustomers";
                var result = await client.GetStringAsync(uri);

                var CustomerList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(result);
            }
        }

        private async void CheckConectionStatus()
        {
            List<Model.ActivationDetails> lstActivationDetails = TAILORING_DB.Instance.GetActivationDetails();
            if (lstActivationDetails.Count > 0)
            {
                Service.clsSmartTailoringService service = new Service.clsSmartTailoringService();
                var result = await service.CheckServerConnection(lstActivationDetails[0].ServerIP);
                if (result)
                {
                    lblConnectionStatus.Text = "Online";
                    lblConnectionStatus.TextColor = Color.Green;
                }
                else
                {
                    lblConnectionStatus.Text = "Offline";
                    lblConnectionStatus.TextColor = Color.Maroon;
                }
            }
        }

        private async void btnPostData_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                // fill the data into object
                Employee employee = new Employee();
                employee.Name = "Test new";
                //employee.Aage =23;

                // serialized the object. ( Convert to JSON string)
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(employee);

                // set the URL
                var url = "http://192.168.43.116/api/Masetr/SaveEmployee";

                // set the content
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // post the data and get the response.
                HttpResponseMessage response = await client.PostAsync(url, content);

                //extract the response in json string format.
                string strResponse = await response.Content.ReadAsStringAsync();

                // get the Reponse in Class-object format. ( Deserialed it)
                Response responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(strResponse);
                if (responseData.Result)// check the values
                {

                }
            }
        }

        private void btnLogs_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new frmLogs());
        }

        private void btnOrder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Customer());
        }

        private async void btnSync_Clicked(object sender, EventArgs e)
        {
            // Get Last SyncID of the customer.
            // last ID says that : this is my updated version.. do you have higher than this..
            int LastChangeID = DAL.TAILORING_DB.Instance.CustomerLastChangeID();

            // Get all new/updated data from Server
            // await keyword is important.. if you remove await you wont get response here.
            var lstCustomerData = await ObjService.GetCustomerDataByLastChangeID(LastChangeID);

            if (lstCustomerData.Count > 0)
            {   // Start Sync operation
                Model.clsSyncOperation ObjSyncOperation = new clsSyncOperation();

                ObjSyncOperation.SyncCustomerData(lstCustomerData, false);
            }
        }

        private void btnSetting_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new View.frmMessageBox());
        }
    }
}