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


            return _database.Query<tblCustomer>("select * from  tblCustomer where MobileNo like '" + mobile + "%'");
        }
        public List<tblCustomer> GetNonSyncCustomer()
        {
          

            return _database.Query<tblCustomer>("select * from  tblCustomer where LastChange=0");
        }

        public int CustomerLastChangeID()
        {
            int LastID = 0;

            LastID = _database.Table<tblCustomer>().Max(x => x.LastChange);



            return LastID;

        }

        public bool  IsCustomerExists(string customerID)
        {
            

            var result= _database.Query<tblCustomer>("select CustomerID from  tblCustomer where CustomerID=" + customerID);
            if (result.Count>0)
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


            return _database.Query<tblCustomer>("select * from  tblCustomer where Name like '%" + name + "%'");
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
        #endregion

        public  void CreateTables()
        {
            try
            {
                CreateActivationDetails();
                CreateCustomerDetails();
                CreateUserManagement();
            }
            catch (Exception ex)
            {
                Utility.WriteLog("CreateTables : "+ex.ToString());


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
        #endregion

        public  int SaveActivationDetails(ActivationDetails activationDetails)
        {
            // Delete all the records, as there should be only one row for activation
            _database.DeleteAll<ActivationDetails>();

            return _database.Insert(activationDetails);
        }

        public  List<ActivationDetails> GetActivationDetails()
        {
            return _database.Table<ActivationDetails>().ToList();

          
        }
        public int DeleteActivation()
        {
            // Delete all the records, as there should be only one row for activation
          return  _database.DeleteAll<ActivationDetails>();



        }

       



    }
}
