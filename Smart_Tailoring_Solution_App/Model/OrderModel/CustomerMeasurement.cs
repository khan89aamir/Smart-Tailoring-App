using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model.OrderModel
{
    public class CustomerMeasurement
    {
        public int SalesOrderID { get; set; }
        public int MasterGarmentID { get; set; }
        public int GarmentID { get; set; }
        public int MeasurementID { get; set; }
        public decimal MeasurementValue { get; set; }
        public int CreatedBy { get; set; }
    }
}