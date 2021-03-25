using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using System.Threading.Tasks;
using SQLite;
using System.Linq;
using Smart_Tailoring_Solution_App.Model;
namespace Smart_Tailoring_Solution_App.DAL
{
    public sealed class TAILORING_DB
    {
        const string DBNAME = "TAILORING_01.db3";
        private static TAILORING_DB instance = null;
        private static readonly object padlock = new object();

        readonly SQLiteConnection _database;

        clsCommon cls = new clsCommon();
        TAILORING_DB()
        {
        }
        public static TAILORING_DB Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TAILORING_DB
                            (
                             Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DBNAME)
                            );
                    }
                    return instance;
                }
            }
        }

        public TAILORING_DB(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
        }

        #region Customer
        public List<tblCustomer> GetCustomerAsync()
        {
            return _database.Table<tblCustomer>().OrderByDescending(x => x.LastChange).ToList();
        }

        public List<tblCustomer> GetCustoemrByMobile(string mobile)
        {
            return _database.Query<tblCustomer>("SELECT * FROM tblCustomer WHERE MobileNo like '" + mobile + "%'");
        }

        public int DuplicateMobileNo(string mobile)
        {
            int count = 0;
            count = _database.Query<tblCustomer>("SELECT * FROM tblCustomer WHERE MobileNo ='" + mobile + "'").ToList().Count;
            return count;
        }

        public List<tblCustomer> GetNonSyncCustomer()
        {
            string str = "select * from  tblCustomer where LastChange = 0";
            return _database.Query<tblCustomer>(str);
        }

        public int CustomerLastChangeID()
        {
            int LastID = 0;
            int count = _database.Table<tblCustomer>().OrderByDescending(x => x.LastChange).ToList().Count;
            if (count != 0)
            {
                LastID = _database.Table<tblCustomer>().Max(x => x.LastChange);
            }
            return LastID;
        }

        public bool IsCustomerExists(string customerID)
        {
            var result = _database.Query<tblCustomer>("SELECT CustomerID FROM tblCustomer WHERE CustomerID=" + customerID);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<tblCustomer> GetCustoemrByName(string name)
        {
            return _database.Query<tblCustomer>("SELECT * FROM  tblCustomer WHERE Name like '%" + name + "%'");
        }

        public int SaveCustomer(tblCustomer customer)
        {
            return _database.Insert(customer);
        }

        public int DeleteCustomerAsync(tblCustomer customer)
        {
            return _database.Delete(customer);
        }

        public int UpdateCustomer(tblCustomer customer)
        {
            return _database.Update(customer);
        }
        public int UpdateCustomerList(List<tblCustomer> lstcustomer)
        {
            return _database.UpdateAll(lstcustomer);
        }

        public int SaveUserManagement(UserManagement user)
        {
            return _database.Insert(user);
        }
        public int UpdateUserManagement(UserManagement user)
        {
            return _database.Update(user);
        }
        public int UpdateUserManagementList(List<UserManagement> lstuser)
        {
            return _database.UpdateAll(lstuser);
        }
        public bool IsUserExists(int UserID)
        {
            var result = _database.Query<tblCustomer>("SELECT UserID FROM tblUserManagement WHERE UserID=" + UserID);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<UserManagement> GetNonSyncUser()
        {
            string str = "SELECT * FROM tblUserManagement WHERE LastChange = 0";
            return _database.Query<UserManagement>(str);
        }
        public List<UserManagement> GetAllUsers()
        {
            string str = "SELECT * FROM tblUserManagement";
            return _database.Query<UserManagement>(str);
        }
        public List<UserManagement> GetLoginUser(int UserID)
        {
            string str = "SELECT * FROM tblUserManagement WHERE UserID = "+ UserID;
            return _database.Query<UserManagement>(str);
        }
        public int UserLastChangeID()
        {
            int LastID = 0;
            int count = _database.Table<UserManagement>().OrderByDescending(x => x.LastChange).ToList().Count;
            if (count != 0)
            {
                LastID = _database.Table<UserManagement>().Max(x => x.LastChange);
            }
            return LastID;
        }
        public int ValidateLocalLogin(string strUserName, string strPassword)
        {
            //string str = "SELECT UserID FROM tblUserManagement WHERE UserName = '" + strUserName + "' AND Password = '" + cls.Encrypt(strPassword, true) + "' AND ActiveStatus=1";
            string str = "SELECT UserID FROM tblUserManagement WHERE UserName = '" + strUserName + "' AND Password = '" + cls.Encrypt(strPassword, true) + "'";
            List<UserManagement> lstUser = _database.Query<UserManagement>(str);
            if (lstUser.Count > 0)
            {
                Service.clsSmartTailoringService.UserID = lstUser[0].UserID;
            }
            return lstUser.Count;
        }
        #endregion

        public void CreateTables()
        {
            try
            {
                CreateActivationDetails();
                CreateCustomerDetails();
                CreateUserManagement();
                //CreateEmployeeDetails();
            }
            catch (Exception ex)
            {
                Utility.WriteLog("CreateTables : " + ex.ToString());
            }
        }

        #region Create Tables
        private CreateTableResult CreateActivationDetails()
        {
            return _database.CreateTable<ActivationDetails>();
        }

        private CreateTableResult CreateCustomerDetails()
        {
            return _database.CreateTable<tblCustomer>();
        }

        private CreateTableResult CreateUserManagement()
        {
            return _database.CreateTable<UserManagement>();
        }

        private CreateTableResult CreateEmployeeDetails()
        {
            return _database.CreateTable<Employee>();
        }
        #endregion

        public int SaveActivationDetails(ActivationDetails activationDetails)
        {
            // Delete all the records, as there should be only one row for activation
            _database.DeleteAll<ActivationDetails>();

            return _database.Insert(activationDetails);
        }

        public List<ActivationDetails> GetActivationDetails()
        {
            return _database.Table<ActivationDetails>().ToList();
        }

        public int DeleteActivation()
        {
            // Delete all the records, as there should be only one row for activation
            return _database.DeleteAll<ActivationDetails>();
        }
    }
}