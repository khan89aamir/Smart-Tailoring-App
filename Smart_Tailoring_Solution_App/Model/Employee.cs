using Android.Media;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model
{
    [Table("EmployeeDetails")]
    public class Employee
    {
        [PrimaryKey]
        public int EmpID { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public Image Photo { get; set; }
        public string EmployeeType { get; set; }
        public string ActiveStatus { get; set; }
        public int LastChange { get; set; }
        public string Address { get; set; }
    }
}
