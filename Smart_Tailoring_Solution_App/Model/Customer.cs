using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Smart_Tailoring_Solution_App.Model
{
    [Table("tblCustomer")]
    public class tblCustomer
    {
        [PrimaryKey,AutoIncrement]
        public int MB_CustomerID { get; set; }
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }

        public string EmailID { get; set; }

        public string Address { get; set; }

        public int LastChange { get; set; } 
    }
}