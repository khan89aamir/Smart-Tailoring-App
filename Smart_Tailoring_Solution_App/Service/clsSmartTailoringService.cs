﻿using System;
        public async Task<bool> CheckServerConnection()
                    URL = "http://" + ServerIPAddress + "/api/Masters/GetStatus";

                    var result = await client.GetStringAsync(URL);

            }

        public async Task<List<tblCustomer>> QuickPostNew_UpdateCustomerToServer(tblCustomer customer)


                    // serialized the object. ( Convert to JSON string)
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(customer);

                    // set the content
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // post the data and get the response.
                    HttpResponseMessage response = await client.PostAsync(URL, content);

                    //extract the response in json string format.
                    string strResponse = await response.Content.ReadAsStringAsync();

                    // get the Response in Class-object format. ( DeSerialized it)
                    lstCustomerResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tblCustomer>>(strResponse);