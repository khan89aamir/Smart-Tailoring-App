using System;using System.Collections;
using System.Collections.Generic;using System.Net.Http;using System.Text;using System.Threading.Tasks;using Smart_Tailoring_Solution_App.DAL;using Smart_Tailoring_Solution_App.Model;namespace Smart_Tailoring_Solution_App.Service{
    public class clsSmartTailoringService    {        public string URL { get; set; }        public static string ServerIPAddress { get; set; }
        public static int UserID { get; set; }

        public async Task<bool> CheckServerConnection(string ServerIP)        {            bool conresult = false;            using (var client = new HttpClient())            {                try                {
                    URL = "http://" + ServerIP + "/api/Masters/GetStatus";
                    var result = await client.GetStringAsync(URL);                    var status = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Status>(result);                    if (status.Result)                    {                        conresult = true;                    }                }                catch (Exception ex)                {                    conresult = false;                }            }            return conresult;        }
        public async Task<bool> CheckServerConnection()        {            bool conresult = false;            using (var client = new HttpClient())            {                try                {
                    URL = "http://" + ServerIPAddress + "/api/Masters/GetStatus";
                    var result = await client.GetStringAsync(URL);                    var status = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Status>(result);                    if (status.Result)                    {                        conresult = true;                    }                }                catch (Exception ex)                {                    conresult = false;                }
            }            return conresult;        }        public async Task<bool> SendActivationRequest(string ServerIP, string SerialNo)        {            bool conresult = false;            using (var client = new HttpClient())            {                try                {                    Model.ActivationDetails activationDetails = new Model.ActivationDetails();                    activationDetails.DeviceSerialNumber = SerialNo;

                    // serialized the object. ( Convert to JSON string)
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(activationDetails);                    URL = "http://" + ServerIP + "/api/Masters/ProcessActivationRequest";

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    Response responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(strResponse);                    if (responseData.Result)// check the values
                    {                        conresult = true;                    }                    else                    {                        conresult = false;                    }                }                catch (Exception ex)                {                    conresult = false;                }            }            return conresult;        }        public async Task<bool> ValidateActivation(string SerialNo, string ActivatonCode, string ServerIP)        {            bool conresult = false;            using (var client = new HttpClient())            {                try                {                    Model.ActivationDetails activationDetails = new Model.ActivationDetails();                    activationDetails.DeviceSerialNumber = SerialNo;                    activationDetails.ActivationCode = ActivatonCode;

                    // serialized the object. ( Convert to JSON string)
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(activationDetails);                    URL = "http://" + ServerIP + "/api/Masters/ValidateActivation";

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    Response responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(strResponse);                    if (responseData.Result)// check the values
                    {                        conresult = true;                    }                    else                    {                        conresult = false;                    }                }                catch (Exception ex)                {                    conresult = false;                }            }            return conresult;        }

        public async Task<List<tblCustomer>> GetCustomerDataByLastChangeID(int LastChangeID)        {            List<tblCustomer> lstcustomerdata = new List<tblCustomer>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Masters/GetCustomerDetails?lastChange=" + LastChangeID;                    var CustomerData = await client.GetStringAsync(URL);

                    // get the customer from server
                    lstcustomerdata = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tblCustomer>>(CustomerData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetCustomerDataByLastChangeID : " + ex.ToString());                }            }            return lstcustomerdata;        }

        public async Task<List<UserManagement>> GetUserDataByLastChangeID(int LastChangeID)        {            List<UserManagement> lstuserdata = new List<UserManagement>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Masters/GetUserManagementDetails?lastChange=" + LastChangeID;                    var UserData = await client.GetStringAsync(URL);

                    // get the customer from server
                    lstuserdata = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserManagement>>(UserData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetUserDataByLastChangeID : " + ex.ToString());                }            }            return lstuserdata;        }

        public async Task<List<tblCustomer>> QuickPostNew_UpdateCustomerToServer(tblCustomer customer)        {            List<tblCustomer> lstCustomerResponse = new List<tblCustomer>();            using (var client = new HttpClient())            {                try                {
                    // serialized the object. ( Convert to JSON string)
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(customer);                    string URL = "http://" + ServerIPAddress + "/api/Masters/Sync_CustomerData";

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    lstCustomerResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tblCustomer>>(strResponse);                }                catch (Exception ex)                {                }            }            return lstCustomerResponse;        }        public async Task<List<tblCustomer>> PostNew_UpdatefCustomerToServer(List<tblCustomer> customer)        {            List<tblCustomer> lstCustomerResponse = new List<tblCustomer>();            using (var client = new HttpClient())            {                try                {
                    // serialized the object. ( Convert to JSON string)
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(customer);                    string URL = "http://" + ServerIPAddress + "/api/Masters/Sync_CustomerData";

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    lstCustomerResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tblCustomer>>(strResponse);                }                catch (Exception ex)                {                }
            }            return lstCustomerResponse;        }        public async Task<List<UserManagement>> PostNew_UpdateUserToServer(List<UserManagement> user)        {            List<UserManagement> lstUserResponse = new List<UserManagement>();            using (var client = new HttpClient())            {                try                {
                    // serialized the object. ( Convert to JSON string)
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(user);                    string URL = "http://" + ServerIPAddress + "/api/Masters/Sync_UserManagementData";

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    lstUserResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserManagement>>(strResponse);                }                catch (Exception ex)                {                }            }            return lstUserResponse;        }        public async Task<bool> ValidateLogin(string strUserName, string strPassword, string ServerIP)        {            bool conresult = false;            using (var client = new HttpClient())            {                try                {                    Model.UserManagement UserDetails = new Model.UserManagement();                    UserDetails.UserName = strUserName;                    UserDetails.Password = strPassword;

                    // serialized the object. ( Convert to JSON string)
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(UserDetails);                    URL = "http://" + ServerIP + "/api/Masters/ValidateLogin";

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    Response responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(strResponse);                    if (responseData.Result)// check the values
                    {                        UserID = responseData.Value;                        conresult = true;                    }                    else                    {                        conresult = false;                    }                }                catch (Exception ex)                {                    Utility.WriteLog("ValidateLogin : " + ex.ToString());                    conresult = false;                }            }            return conresult;        }        public async Task<List<Employee>> GetEmployeeDetails(int EmpID)        {            List<Employee> lstEmployeedata = new List<Employee>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Masters/GetEmployeeDetails?EmpID=" + EmpID;                    var EmployeeData = await client.GetStringAsync(URL);

                    // get the Employees from server
                    lstEmployeedata = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Employee>>(EmployeeData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetEmployeeDetails : " + ex.ToString());                }            }            return lstEmployeedata;        }


        public async Task<List<Model.OrderModel.clsMeasurment>> GetGarmentMasterMeasurement(int GarmentID)        {            List<Model.OrderModel.clsMeasurment> lstMeasurment = new List<Model.OrderModel.clsMeasurment>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Masters/GetGarmentMasterMeasurement?GarmentID=" + GarmentID;                    var MeasurmentData = await client.GetStringAsync(URL);

                    // get the Employees from server
                    lstMeasurment = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.OrderModel.clsMeasurment>>(MeasurmentData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetGarmentMasterMeasurement : " + ex.ToString());                }            }            return lstMeasurment;        }        public async Task<List<Model.OrderModel.clsGarmentRate>> GetGarmentRate(int GarmentID, int ServiceType = 0)        {            List<Model.OrderModel.clsGarmentRate> lstGarmentRate = new List<Model.OrderModel.clsGarmentRate>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Order/GetGarmentRate?GarmentID=" + GarmentID + "&ServiceType=" + ServiceType;                    var GarmentRateData = await client.GetStringAsync(URL);

                    // get the Garment Rate Data from server
                    lstGarmentRate = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.OrderModel.clsGarmentRate>>(GarmentRateData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetGarmentRate : " + ex.ToString());                }            }            return lstGarmentRate;        }        public async Task<List<Model.GarmentList>> GetGarmentList(int GarmentID = 0)        {            List<Model.GarmentList> lstGarmentList = new List<Model.GarmentList>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Order/GetGarmentList?GarmentID=" + GarmentID;                    var GarmentListData = await client.GetStringAsync(URL);

                    // get the Garment List Data from server
                    lstGarmentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.GarmentList>>(GarmentListData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetGarmentList : " + ex.ToString());                }            }            return lstGarmentList;        }

        public async Task<List<Model.OrderModel.StitchType>> GetStitchType()        {            List<Model.OrderModel.StitchType> lstStitchTypeList = new List<Model.OrderModel.StitchType>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Order/GetStitchType";                    var StitchTypeData = await client.GetStringAsync(URL);

                    // get the Garment List Data from server
                    lstStitchTypeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.OrderModel.StitchType>>(StitchTypeData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetStitchType : " + ex.ToString());                }            }            return lstStitchTypeList;        }

        public async Task<List<Model.OrderModel.FitType>> GetFitType()        {            List<Model.OrderModel.FitType> lstFitTypeList = new List<Model.OrderModel.FitType>();            using (var client = new HttpClient())            {                try                {                    string URL = "http://" + ServerIPAddress + "/api/Order/GetFitType";                    var FitTypeData = await client.GetStringAsync(URL);

                    // get the Garment List Data from server
                    lstFitTypeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.OrderModel.FitType>>(FitTypeData);
                }                catch (Exception ex)                {                    Utility.WriteLog("GetFitType : " + ex.ToString());                }            }            return lstFitTypeList;        }

        //Testing
        public async Task<bool> SetMeasurementStyle(Model.OrderModel.clsMeasurment measure,Model.OrderModel.clsStyle style)        {            bool conresult = false;            using (var client = new HttpClient())            {                try                {
                    ArrayList paramList = new ArrayList();
                    paramList.Add(measure);
                    paramList.Add(style);

                    // serialized the object. ( Convert to JSON string)
                    //var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(UserDetails);                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(paramList);                    URL = "http://" + ServerIPAddress + "/api/Order/GetMeasurmentStyle";

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    Response responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(strResponse);                    if (responseData.Result)// check the values
                    {                        conresult = true;                    }                    else                    {                        conresult = false;                    }                }                catch (Exception ex)                {                    Utility.WriteLog("SetMeasurementStyle : " + ex.ToString());                    conresult = false;                }            }            return conresult;        }    }}