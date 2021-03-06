﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Tailoring_Solution_App.Model
{
    [Table("tblUserManagement")]
    public class UserManagement
    {
        [PrimaryKey, AutoIncrement]
        public int MB_UserID { get; set; }
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string EmailID { get; set; }
        public int LastChange { get; set; }
        public int ActiveStatus { get; set; }

        public int EmployeeID { get; set; }
    }
}