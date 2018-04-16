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
using Parallel.Test.Framework.Base.TestSettings;
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
        public IWebDriver Driver { get; set; }
        public Dictionary<string, string> Environment { get; set; }

        public void OpenBrowser() {
            var runTests = TestSettings[TestSettingsConst.RunTest];
            if (string.Equals(runTests, RunTest.Local.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                var br = new Browser();
                Driver = br.InitializeBrowser(Assembly.Directory, TestSettings[TestSettingsConst.Browser]);
                Driver.Manage().Window.Maximize();
                ExtentTestManager.GetTest().Log(Status.Info, "Opened Browser" + TestSettings[TestSettingsConst.Browser]);
            }
            else if (string.Equals(runTests, RunTest.BrowserStack.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                var caps = new DesiredCapabilities();
                var browser = TestSettings[TestSettingsConst.Browser]; //"chrome";
                var version = TestSettings[TestSettingsConst.Version]; //"45";
                var os = TestSettings[TestSettingsConst.Os]; //"Windows 7";
                var deviceName = TestSettings[TestSettingsConst.DeviceName]; //"";
                var deviceOrientation = TestSettings[TestSettingsConst.DeviceOrientation]; //"";
                caps.SetCapability(CapabilityType.BrowserName, browser);
                caps.SetCapability(CapabilityType.Version, version);
                caps.SetCapability(CapabilityType.Platform, os);
                caps.SetCapability("deviceName", deviceName);
                caps.SetCapability("deviceOrientation", deviceOrientation);
                caps.SetCapability("username", TestSettings[TestSettingsConst.Username]); //"aadhithbose");
                caps.SetCapability("accessKey", TestSettings[TestSettingsConst.AccessKey]); //"1cc813ac-bec2-4dd8-9e9e-a239ec2e7c2c");
                caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

                Driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            }
        }

        public void CloseBrowser() {
            ExtentTestManager.GetTest().Log(Status.Info, "", MediaEntityBuilder.CreateScreenCaptureFromPath(Driver.Capture()).Build());
            Driver.Quit();
        }

        public void FetchTestData(string testSource, string testCaseId, string testDataNo) {
            TestData = new Dictionary<string, string>();
            var jsonLib = new LoadJsonLib();
            var o = jsonLib.LoadJson(testSource);
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