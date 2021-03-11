using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model
{
    [Table("tblActivationDetails")]
    public class ActivationDetails
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ServerIP { get; set; }
        public string ActivationCode { get; set; }

        public string DeviceSerialNumber { get; set; }
    }
}