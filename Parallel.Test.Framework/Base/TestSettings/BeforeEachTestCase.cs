using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using AventStack.ExtentReports;
using ExcelDataReader;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Parallel.Test.Framework.Base.Environment.Browser;
using Parallel.Test.Framework.Base.Reports;
using Parallel.Test.Framework.Constants;
using Parallel.Test.Framework.Lib.Json;

namespace Parallel.Test.Framework.Base.TestSettings {
    public class BeforeEachTestBase {
        public BeforeEachTestBase(Dictionary<string, string> testsettings, Dictionary<string, string> environment) {
            TestSettings = testsettings;
            Environment = environment;
        }

        public Dictionary<string, string> TestSettings { get; set; }

        public Dictionary<string, string> TestData { get; set; }

        //public IWebDriver Driver { get; set; }
        public Dictionary<string, string> Environment { get; set; }

        public IWebDriver OpenBrowser() {
            IWebDriver driver;
            var runTests = TestSettings[TestSettingsConst.RUN_TEST];
            if (string.Equals(runTests, RunTest.Sauce.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                var caps = new DesiredCapabilities();
                var browser = TestSettings[TestSettingsConst.BROWSER]; //"chrome";
                var version = TestSettings[TestSettingsConst.VERSION]; //"45";
                var os = TestSettings[TestSettingsConst.OS]; //"Windows 7";
                var deviceName = TestSettings[TestSettingsConst.DEVICENAME]; //"";
                var deviceOrientation = TestSettings[TestSettingsConst.DEVICEORIENTATION]; //"";
                caps.SetCapability(CapabilityType.BrowserName, browser);
                caps.SetCapability(CapabilityType.Version, version);
                caps.SetCapability(CapabilityType.Platform, os);
                caps.SetCapability("deviceName", deviceName);
                caps.SetCapability("deviceOrientation", deviceOrientation);
                caps.SetCapability("username", TestSettings[TestSettingsConst.USERNAME_SAUCE]);
                caps.SetCapability("accessKey", TestSettings[TestSettingsConst.ACCESS_KEY_SAUCE]);
                caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
                driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            }
            else {
                //if (string.Equals(runTests, RunTest.Local.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                var br = new Browser();
                driver = br.InitializeBrowser(TestSettings);
                driver.Manage().Window.Maximize();
                ExtentTestManager.GetTest().Log(Status.Info, "Opened Browser" + TestSettings[TestSettingsConst.BROWSER]);
            }

            return driver;
        }


        public void CloseBrowser(IWebDriver driver) {
            ExtentTestManager.GetTest().Log(Status.Info, "", MediaEntityBuilder.CreateScreenCaptureFromPath(driver.Capture()).Build());
            driver.Quit();
        }

        public void FetchTestData(string testSource, string testCaseId, string testDataNo = null) {
            TestData = new Dictionary<string, string>();

            var extension = Path.GetExtension(testSource);
            Console.WriteLine(@"extension " + extension);
            if (extension == ".json")
                ReadDataFromJson(testSource, testCaseId, testDataNo);
            else if (extension == ".csv") PopulateInCollection(testSource, testDataNo);
        }

        private void PopulateInCollection(string testSource, string testDataNo) {
            var table = ExcelToDataTable(testSource);

            //Iterate through the rows and columns of the Table
            for (var row = 1; row <= table.Rows.Count; row++)
                for (var col = 0; col < table.Columns.Count; col++) {
                    if(row==int.Parse(testDataNo))
                    TestData.Add(table.Columns[col].ColumnName, table.Rows[row - 1][col].ToString());
            }

            Console.WriteLine(@"Test Data Read");
            foreach (KeyValuePair<string, string> keyValuePair in TestData) {
                Console.WriteLine(keyValuePair.Key + @" " + keyValuePair.Value);
            }
        }

        private DataTable ExcelToDataTable(string fileName) {
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration() {
                FallbackEncoding = Encoding.GetEncoding(1252),
                AutodetectSeparators = new[] { ',', ';', '\t', '|', '#' }
            });

            var result = reader.AsDataSet(new ExcelDataSetConfiguration {
                ConfigureDataTable = data => new ExcelDataTableConfiguration {
                    UseHeaderRow = true
                }
            });
            
            //Get all the tables
            var table = result.Tables;
            foreach (DataTable dataTable in table) {
                Console.WriteLine(@"dataTable " + dataTable);
                Console.WriteLine(@"dataTable " + dataTable.DataSet.DataSetName);
            }
            // store it in data table
            var resultTable = table["table1"];
            stream.Close();
            stream.Dispose();
            return resultTable;
        }


        private void ReadDataFromJson(string testSource, string testCaseId, string testDataNo) {
           
            var jsonLib = new JsonLib();
            var o = jsonLib.JObject(testSource);
            var testCaseDetails = o.SelectToken("$.TestCases[?(@.TestCaseId == '" + testCaseId + "')]");
            var testDataDetails = testCaseDetails.SelectToken("$.TestRow[?(@.TestDataNumber == '" + testDataNo + "')]");
            var testData = testDataDetails.SelectToken("$.TestData");

            var abc = testData.ToList();
            foreach (var a in abc)
                //Console.WriteLine(a.Type);
                if (a.Type == JTokenType.Object) {
                    var obj = a.ToObject<Dictionary<string, string>>();
                    foreach (var pair in obj)
                        TestData.Add(pair.Key, pair.Value);
                }
        }
    }
}