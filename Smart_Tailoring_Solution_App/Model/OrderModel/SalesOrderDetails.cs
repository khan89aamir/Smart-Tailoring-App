using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model.OrderModel
{
    public class SalesOrderDetails
    {
        public int SalesOrderID { get; set; }
        public int StichTypeID { get; set; }
        public int FitTypeID { get; set; }
        public int MasterGarmentID { get; set; }
        public int GarmentID { get; set; }
        public int Service { get; set; }
        public DateTime TrailDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TrimAmount { get; set; }
        public int QTY { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public int CreatedBy { get; set; }
    }
}