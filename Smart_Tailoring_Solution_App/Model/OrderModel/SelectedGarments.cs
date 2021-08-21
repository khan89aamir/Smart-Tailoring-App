using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model
{
    public class SelectedGarments
    {

        public int GarmentID { get; set; }

        public string Name { get; set; }

        public int QTY { get; set; }

        public bool IsTrail { get; set; }
        public DateTime TrailDate { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
