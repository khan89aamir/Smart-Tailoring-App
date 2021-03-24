using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model.OrderModel
{
    public class CustomerBodyPosture
    {
        public int SalesOrderID { get; set; }
        public int MasterGarmentID { get; set; }
        public int GarmentID { get; set; }
        public int BodyPostureID { get; set; }
        public int BodyPostureMappingID { get; set; }
        public int CreatedBy { get; set; }
    }
}