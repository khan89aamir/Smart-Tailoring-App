using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model.OrderModel
{
    public class clsGarmentRate
    {
        public int GarmentRateID { get; set; }
        public int GarmentID { get; set; }
        public string GarmentCode { get; set; }
        public string GarmentName { get; set; }
        public string GarmentCodeName { get; set; }
        public string GarmentType { get; set; }
        public double Rate { get; set; }
        public string OrderType { get; set; }
        public int LastChange { get; set; }
    }
}