using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model
{
    public class SelectedGarments : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public int GarmentID { get; set; }
        public string GarmentCode { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }

        public int QTY { get; set; }

        public bool IsTrail { get; set; }
        public DateTime TrailDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        private double _Rate;
        public double Rate 
        {
            get { return _Rate; }
            set
            {
                _Rate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Rate"));
            }
        }
        private double _Trim;
        public double TrimAmount
        {
            get { return _Trim; }
            set
            {
                _Trim = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TrimAmount"));
            }
        }
        public string OrderType { get; set; }
        public int OrderTypeID { get; set; }

        public List<OrderModel.clsGarmentRate> lstGarmentRate { set; get; }
    }
}