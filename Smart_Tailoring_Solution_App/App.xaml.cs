using Smart_Tailoring_Solution_App.Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App
{
    public partial class App : Application
    {


        public App()
        {
            InitializeComponent();



            DAL.TAILORING_DB.Instance.CreateTables();
            Utility.WriteLog("Tables Created");
            // get data from activation table
            List<ActivationDetails> lstActivationDetails = DAL.TAILORING_DB.Instance.GetActivationDetails();
            if (lstActivationDetails.Count > 0)
            {
                Utility.WriteLog("Activation Found ");

                // set the server IP Address so that it can be used 
                Service.clsSmartTailoringService.ServerIPAddress = lstActivationDetails[0].ServerIP;


                if (lstActivationDetails[0].DeviceSerialNumber == ActivationPage.Id.ToUpper())
                {

                    App.Current.MainPage = new View.frmLogin();
                    // App.Current.MainPage = new View.Page1();



                }
                else
                {
                    Utility.WriteLog("Serial Number not matched ! ");

                    MainPage = new ActivationPage();

                }

            }
            else
            {
                Utility.WriteLog("Application not activated. Showing Activation page. ");

                MainPage = new ActivationPage();

            }




        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
