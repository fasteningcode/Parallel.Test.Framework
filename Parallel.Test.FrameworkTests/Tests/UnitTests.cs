using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Parallel.Test.Framework.Base;
using Parallel.Test.Framework.Base.TestSettings;
using Parallel.Test.Framework.Constants;
using Parallel.Test.Framework.Lib.DataBase;

namespace Parallel.Test.FrameworkTests.Tests {
    [TestFixture]
    [Parallelizable]
    public class UnitTests : Framework.Base.Base {
#if (DEBUG)
        [Test]
#endif
        public void DataBase() {
            var dataSouce = @"C:\Code\Automation\MiddlewareTests\Tests\TestCaseData\SLT1.json";
            _.FetchTestData(dataSouce, "SLT1ValidateRetrieveApplication", "1");
            var connectionString = new DBExecute(_.Environment["DbConnUnsecured"]);
            var result = connectionString.ExecuteQuery(_.TestData["DbQuery"]);
            Assert.IsTrue(result != null);
            var result2 = connectionString.ExecuteQuery(_.TestData["DbQuery"]);
            Assert.IsTrue(result2 != null);
            connectionString.CloseDB();
            Assert.IsTrue(result != null);
        }

#if (!DEBUG)
        [Test]
        [Category("UnitTest")]
#endif
        public void DirectoryTest() {
            Console.WriteLine(ExecutionAssembly.Directory);
        }

#if (!DEBUG)
        [Test]
#endif
        public void ReadEnvironmentFromJson() {

            var testConfigs = new ConfigsBeforeEachTestSuite();
            var env = testConfigs.ReadEnvironmentFromJson(ExecutionAssembly.Directory + "/Environment.json", "UAT");
            foreach (var keyValuePair in env) {
                Console.WriteLine(keyValuePair.Key + @" " + keyValuePair.Value);
                if (keyValuePair.Key == "FrontEnd")
                    Assert.AreEqual(keyValuePair.Value, "http://fasteningcode.com/");
            }

            Console.WriteLine(env["DbConnUnsecured"]);
        }

#if (!DEBUG)
        [Test]
#endif
        public void TestSettingsTest() {
            var value = TestSettings[TestSettingsConst.ENVIRONMENT];
            Console.WriteLine(value);
            Assert.IsTrue(value != null);
        }
#if (!DEBUG)
        [Test]
#endif
        public void TestDataJson() {
            var driver = _.OpenBrowser();
            driver.Navigate().GoToUrl(_.Environment["FrontEnd"]);
            _.FetchTestData(ExecutionAssembly.Directory + "/Settings/SampleTestData.json", "tc1", "1");
            driver.FindElement(By.XPath("/html/body/form/input[1]")).SendKeys(_.TestData["username"]);
            driver.FindElement(By.XPath("/html/body/form/input[2]")).SendKeys(_.TestData["password"]);
            driver.FindElement(By.XPath("/html/body/form/input[3]")).Click();
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/h1")).Text.Contains("Welcome"));
            _.CloseBrowser(driver);
        }
#if (!DEBUG)
        [Test]
#endif
        public void TestDataCsv()
        {
            var driver = _.OpenBrowser();
            driver.Navigate().GoToUrl(_.Environment["FrontEnd"]);
            _.FetchTestData(ExecutionAssembly.Directory + "/Settings/SampleTestData.csv", "tc1", "1");
            driver.FindElement(By.XPath("/html/body/form/input[1]")).SendKeys(_.TestData["username"]);
            driver.FindElement(By.XPath("/html/body/form/input[2]")).SendKeys(_.TestData["password"]);
            driver.FindElement(By.XPath("/html/body/form/input[3]")).Click();
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/h1")).Text.Contains("Welcome"));
            _.CloseBrowser(driver);
        }

#if (!DEBUG)
        [Test]
#endif
        public void ConfigsBeforeEachTestSuite_TestSetup() {
            var testSetup = new ConfigsBeforeEachTestSuite();
            var result = testSetup.TestSetup(ExecutionAssembly.Directory + "/TestSettings.json");

            foreach (var v in result) Console.WriteLine(v.Key + @" " + v.Value);
            Assert.True(result != null);
        }
    }
}