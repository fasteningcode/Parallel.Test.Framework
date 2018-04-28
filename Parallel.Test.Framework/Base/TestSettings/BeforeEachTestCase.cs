using System;
using System.Collections.Generic;
using System.Linq;
using AventStack.ExtentReports;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Parallel.Test.Framework.Base.Environment.Browser;
using Parallel.Test.Framework.Base.Reports;
using Parallel.Test.Framework.Constants;
using Parallel.Test.Framework.Lib.Json;

namespace Parallel.Test.Framework.Base {
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
            IWebDriver driver = null;
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
            else
            { //if (string.Equals(runTests, RunTest.Local.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
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

        public void FetchTestData(string testSource, string testCaseId, string testDataNo) {
            TestData = new Dictionary<string, string>();
            var jsonLib = new JsonLib();
            var o = jsonLib.JObject(testSource);
            var testCaseDetails = o.SelectToken("$.TestCases[?(@.TestCaseId == '" + testCaseId + "')]");
            var testDataDetails = testCaseDetails.SelectToken("$.TestRow[?(@.RowNumber == '" + testDataNo + "')]");
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