using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AventStack.ExtentReports;
using Parallel.Test.Framework.Base.Reports;

namespace Parallel.Test.Framework.Lib.DataBase {
    public class DBExecute {
        public DBExecute(string dbConnectionString) {
            DB = new DbBase();
            Conn = DB.DbConnect(dbConnectionString);
            //ExtentTestManager.GetTest().Log(Status.Info, "dbConnectionString " + dbConnectionString);
        }


        private DataTable Result { get; set; }
        private SqlConnection Conn { get; }
        private DbBase DB { get; }

        public DataTable ExecuteQuery(string query) {
            Result = DB.ExecuteQuery(Conn, query);
            ExtentTestManager.GetTest().Log(Status.Info, "DB query " + query);
            return Result;
        }


        public DataTable CloseDB() {
            DB.DbClose(Conn);
            ExtentTestManager.GetTest().Log(Status.Info, "DB Connection Closed");

            return Result;
        }

        public string GetData(DataTable result, string searchDbHeadings, string searchString, string returnDBHeading) {
            object re = null;

            try {
                re = (from DataRow dr in result.Rows
                    where (string) dr[searchDbHeadings] == searchString
                    select dr[returnDBHeading]).FirstOrDefault();
            }
            catch (Exception) {
                // ignored
            }

            if (re == DBNull.Value)
                return false.ToString();
            if (re != null)
                return re.ToString();
            return false.ToString();
        }
    }
}