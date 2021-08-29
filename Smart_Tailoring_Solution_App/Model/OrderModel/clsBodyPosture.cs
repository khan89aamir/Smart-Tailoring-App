using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model.OrderModel
{
    public class clsBodyPosture
    {
        public int BodyPostureID { get; set; }
        public string BodyPostureType { get; set; }
        public List<clsImageList> lstImage { get; set; }
    }
}