using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model
{
    public class GarmentList
    {

        public int GarmentID { get; set; }

        public string ImageURL { get; set; }

        public string Name { get; set; }

        public int QTY { get; set; }

        public DateTime TrailDate { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
