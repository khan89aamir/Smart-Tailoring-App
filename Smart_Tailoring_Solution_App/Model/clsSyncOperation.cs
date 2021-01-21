using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Smart_Tailoring_Solution_App.DAL;

namespace Smart_Tailoring_Solution_App.Model
{
    public class clsSyncOperation
    {
        Service.clsSmartTailoringService ObjService = new Service.clsSmartTailoringService();
        internal async void SyncCustomerData(List<tblCustomer> lstCutomerData, bool isQuick)
        {
            if (!isQuick)
            {
                Utility.WriteLog("Customer Sync Started.....");
                foreach (tblCustomer customer in lstCutomerData)
                {
                    Utility.WriteLog("Customer Sync : " + customer.Name);
                    // check if the customer is new or updated.
                    if (TAILORING_DB.Instance.IsCustomerExists(customer.CustomerID.ToString()))
                    {
                        // update the customer
                        TAILORING_DB.Instance.UpdateCustomer(customer);
                        Utility.WriteLog("Customer Update : Customer ID : " + customer.CustomerID);

                    }
                    else
                    {
                        // add new customer
                        TAILORING_DB.Instance.SaveCustomer(customer);
                        Utility.WriteLog("New Customer Added");
                    }


                }
                Utility.WriteLog("Customer pull operation completed !");
                // after Pulling the new/update data, Push the current Data
            }


            List<tblCustomer> tblCustomers= DAL.TAILORING_DB.Instance.GetNonSyncCustomer();
            bool resultcon=  await ObjService.CheckServerConnection();
            if (resultcon)
            {
                // Push the update or newly added data to the server
                var lstCutomerDataUpdate = await ObjService.PostNew_UpdatefCustomerToServer(tblCustomers);


                // get the LastChange ID and Customer ID
                List<tblCustomer> tblUpdateCustRecord = lstCutomerDataUpdate;

                int finalResult = DAL.TAILORING_DB.Instance.UpdateCustomerList(tblUpdateCustRecord);
                if (finalResult >= 0)
                {
                    Utility.WriteLog("Customer Syn Operation completed ");
                }
            }
            else
            {
                Utility.WriteLog("Not able to connect to the server.");
            }
         
            



        }

    }
}
