using System;
using System.Data;
using System.Data.SqlClient;
using AventStack.ExtentReports;
using Parallel.Test.Framework.Base.Reports;

namespace Parallel.Test.Framework.Lib.DataBase {
    public  class DbBase {
        //private SqlConnection sqlConnection;
        public SqlConnection DbConnect(string connectionString) {
            try {
                ExtentTestManager.GetTest().Log(Status.Info, "connectionString " + connectionString);
                var sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (Exception e) {
                Console.WriteLine($@"{Status.Info} Error {e.Message}");
            }

            return null;
        }

        public void DbClose(SqlConnection sqlConnection) {
            try {
                sqlConnection.Close();
            }
            catch (Exception e) {
                ExtentTestManager.GetTest().Log(Status.Info, "Error :: " + e.Message);
            }
        }

        //perform some exection
        public DataTable ExecuteQuery(SqlConnection sqlConnection, string queryString) {
            DataSet dataSet;
            try {
                //checking for state of connection
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (sqlConnection == null || sqlConnection != null && (sqlConnection.State == ConnectionState.Closed ||
                                                                       sqlConnection.State == ConnectionState.Broken))
                    if (sqlConnection != null)
                        sqlConnection.Open();

                var dataAdaptor = new SqlDataAdapter {
                    SelectCommand = new SqlCommand(queryString, sqlConnection) {CommandType = CommandType.Text}
                };

                dataSet = new DataSet();
                dataAdaptor.Fill(dataSet, "table");
                if (sqlConnection != null) sqlConnection.Close();
                return dataSet.Tables["table"];
            }
            catch (Exception e) {
                // ReSharper disable once RedundantAssignment
                dataSet = null;
                // ReSharper disable once PossibleNullReferenceException
                sqlConnection.Close();
                //Base.Base.ExtentTestManager.GetTest().Log(Status.Info, "Error :: " + e.Message);
                // ReSharper disable once LocalizableElement
                Console.WriteLine(Status.Info + "Error :: " + e.Message);

                return null;
            }
            finally {
                // ReSharper disable once PossibleNullReferenceException
                sqlConnection.Close();
                // ReSharper disable once RedundantAssignment
                dataSet = null;
            }
        }

       
    }
}