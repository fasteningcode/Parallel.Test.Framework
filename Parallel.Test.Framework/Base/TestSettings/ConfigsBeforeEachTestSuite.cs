using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;
using Parallel.Test.Framework.Constants;
using Parallel.Test.Framework.Lib.Json;

namespace Parallel.Test.Framework.Base.TestSettings {
    public class ConfigsBeforeEachTestSuite {
        public Dictionary<string, string> ReadEnvironmentFromJson(string envJsonPath, string envName) {
            var data = new Dictionary<string, string>();
            var jsonLib = new LoadJsonLib();
            var o = jsonLib.LoadJson(envJsonPath);
            var testCaseDetails = o.SelectToken("$.TestEnv[?(@.EnvName == '" + envName + "')]");
            //JToken testDataDetails = testCaseDetails.SelectToken("$.TestRow[?(@.RowNumber == '" + testDataNo + "')]");
            var testData = testCaseDetails.SelectToken("$.EnvDetails");

            var abc = testData.ToList();
            foreach (var a in abc)
                //Console.WriteLine(a.Type);
                if (a.Type == JTokenType.Object) {
                    var obj = a.ToObject<Dictionary<string, string>>();
                    foreach (var pair in obj)
                        data.Add(pair.Key, pair.Value);
                }

            return data;
        }

        public Dictionary<string, string> TestSetup(string xmlPath) {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            var r = new Dictionary<string, string>();

            var environment = xmlDoc.GetElementsByTagName(TestSettingsConst.Environment);
            r.Add(TestSettingsConst.Environment, environment[0].InnerText);

            r.Add(TestSettingsConst.RunTest, xmlDoc.GetElementsByTagName(TestSettingsConst.RunTest)[0].InnerText);
            r.Add(TestSettingsConst.Browser, xmlDoc.GetElementsByTagName(TestSettingsConst.Browser)[0].InnerText);
            r.Add(TestSettingsConst.Version, xmlDoc.GetElementsByTagName(TestSettingsConst.Version)[0].InnerText);
            r.Add(TestSettingsConst.OS, xmlDoc.GetElementsByTagName(TestSettingsConst.OS)[0].InnerText);
            r.Add(TestSettingsConst.DeviceName, xmlDoc.GetElementsByTagName(TestSettingsConst.DeviceName)[0].InnerText);
            r.Add(TestSettingsConst.DeviceOrientation, xmlDoc.GetElementsByTagName(TestSettingsConst.DeviceOrientation)[0].InnerText);
            r.Add(TestSettingsConst.username, xmlDoc.GetElementsByTagName(TestSettingsConst.username)[0].InnerText);
            r.Add(TestSettingsConst.accessKey, xmlDoc.GetElementsByTagName(TestSettingsConst.accessKey)[0].InnerText);
            
            r.Add(TestSettingsConst.DefaultWaitinSeconds, xmlDoc.GetElementsByTagName("DefaultWaitinSeconds")[0].InnerText); 
            r.Add(TestSettingsConst.WaitForFirstElemtentinMinutes,
                xmlDoc.GetElementsByTagName("WaitForFirstElemtentinPageinMinutes")[0].InnerText); 


            r.Add("",xmlDoc.GetElementsByTagName("ReportPath")[0].InnerText); 

            //EnvConfig.RecordTest = Convert.ToBoolean(xmlDoc.GetElementsByTagName("RecordTest")[0].InnerText); // false;//Convert.ToBoolean(ExcelLibHelpers.ReadData(2, "Record Test"));
            //EnvConfig.ReportScreenshot = Convert.ToBoolean(xmlDoc.GetElementsByTagName("ReportScreenshot")[0].InnerText); // true;//Convert.ToBoolean(ExcelLibHelpers.ReadData(2, "Report Screenshot"));
            //EnvConfig.ReportScreenshotAtBeginningOfEachStep =
            //    Convert.ToBoolean(xmlDoc.GetElementsByTagName("Screenshotatbeginningofeachstep")[0]
            //        .InnerText); // true; //Convert.ToBoolean(ExcelLibHelpers.ReadData(2, "Screenshot at beginning of each step"));
            ////Settings.TestExecutionNumber = int.Parse(ExcelLibHelper.ReadData(2, "TestExecutionNumber"));
            //EnvConfig.ExcelVisible =
            //    Convert.ToBoolean(xmlDoc.GetElementsByTagName("ExcelVisibility")[0]
            //        .InnerText); // false; //Convert.ToBoolean(ExcelLibHelpers.ReadData(2, "Excel Visibility")); // TestEnvironmentSettings.ExcelVisible;
            //EnvConfig.TempFolder = xmlDoc.GetElementsByTagName("TempFolder")[0].InnerText; //"D:\\Tp"; //ExcelLibHelpers.ReadData(2, "Temp Folder");


            //#endregion

            //#region ReadingAdvanceSetupValues

            //EnvConfig.ExcelDefaultRow = int.Parse(xmlDoc.GetElementsByTagName("ExcelDefaultRow")[0].InnerText); //2; //int.Parse(ExcelLibHelpers.ReadData(2, ExcelDefaultRow));
            //Settings.CurrentRowNum = EnvConfig.ExcelDefaultRow;
            //EnvConfig.FullScrenshot = Convert.ToBoolean(xmlDoc.GetElementsByTagName("FullScreenshot")[0].InnerText); // false;//Convert.ToBoolean(ExcelLibHelpers.ReadData(FullScreenshot));

            //EnvConfig.MongoHost = xmlDoc.GetElementsByTagName("MongoHost")[0].InnerText; // "localhost";//ExcelLibHelpers.ReadData(MongoHost);
            //EnvConfig.MongoPort = int.Parse(xmlDoc.GetElementsByTagName("MongoPort")[0].InnerText); //i27017;//int.Parse(ExcelLibHelpers.ReadData(MongoPort));
            //EnvConfig.ReportServerURL = xmlDoc.GetElementsByTagName("ReportServerURL")[0].InnerText; //"http://localhost:1337/#/"; //ExcelLibHelpers.ReadData(ReportServerURL);

            //EnvConfig.HtmlReportDashboard =
            //    Convert.ToBoolean(xmlDoc.GetElementsByTagName("HTMLReportDashboard")[0].InnerText); //false;//Convert.ToBoolean(ExcelLibHelpers.ReadData(HTMLReportDashboard));
            //EnvConfig.Configuration = xmlDoc.GetElementsByTagName("Configuration")[0].InnerText; // "Release"; //ExcelLibHelpers.ReadData(Configuration);
            //EnvConfig.Platform = xmlDoc.GetElementsByTagName("Platform")[0].InnerText; //"Any CPU";// ExcelLibHelpers.ReadData(Platform);
            //EnvConfig.KillTasks = xmlDoc.GetElementsByTagName("KillTasks")[0].InnerText; //"chromedriver.exe";

            //ExcelConfig.GenericLoanCalculatorExcelPath =
            //    Settings.CurrentDirectory + "\\Data\\EnvironmentSetup\\TCDataClearmatch\\Generic Loan Calculator v 1 23.xls"; //ExcelLibHelpers.ReadData(GenericLoanCalculatorExcelPath);
            //ExcelConfig.SurplusCalculatorExcelPath =
            //    Settings.CurrentDirectory + @"\Data\EnvironmentSetup\TCDataClearmatch\Surplus Calculator 2017.xlsx"; //ExcelLibHelpers.ReadData(SurplusCalculatorExcelPath);
            //ExcelConfig.VedaCustomerPath = Settings.CurrentDirectory + @"\Data\EnvironmentSetup\TCDataWebApp\VedaCustomerList.xlsx"; //ExcelLibHelpers.ReadData(VedaCustomerPath);
            //ExcelConfig.DBCustomerPath = Settings.CurrentDirectory + @"\Data\EnvironmentSetup\TCDataWebApp\DBCustomerList.xlsx"; //ExcelLibHelpers.ReadData(DBCustomerPath);
            //ExcelConfig.AustraliaIncomeCalculatorExcelPath =
            //    Settings.CurrentDirectory + @"\Data\EnvironmentSetup\TCDataClearmatch\Australia-Personal-Income-Tax-Calculator.xlsx"; //ExcelLibHelpers.ReadData(IncomeCalculatorPath);
            //ExcelConfig.AustraliaIncomeCalculatorExcelPathLevyOff =
            //    Settings.CurrentDirectory + @"\Data\EnvironmentSetup\TCDataClearmatch\Australia-Personal-Income-Tax-Calculator-LevyOff.xlsx"; //ExcelLibHelpers.ReadData(IncomeCalculatorPathLevyoff);
            //ExcelConfig.PTPCalcExcelPath = Settings.CurrentDirectory + @"\Data\EnvironmentSetup\TCDataClearmatch\Promise To Pay Calculator v 2 12.xls"; //ExcelLibHelpers.ReadData(PTPCalcExcelPath);


            //ExcelConfig.GenericLoanCalculatorExcelName = Path.GetFileName(ExcelConfig.GenericLoanCalculatorExcelPath);
            //ExcelConfig.SurplusCalculatorExcelName = Path.GetFileName(ExcelConfig.SurplusCalculatorExcelPath);
            ////ExcelConfig.InitialSettingsExcelName = Path.GetFileName(ExcelConfig.InitialSettingsExcelPath);

            //#endregion
            return r;
        }
    }
}