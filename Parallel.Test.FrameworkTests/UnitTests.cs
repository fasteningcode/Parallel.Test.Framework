using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Parallel.Test.Framework.Base;
using Parallel.Test.Framework.Base.TestSettings;
using Parallel.Test.Framework.Lib.DataBase;

namespace Parallel.Test.FrameworkTests {
    [TestFixture]
    [Parallelizable]
    public class UnitTests : Base {
#if (DEBUG)
        [Test]
#endif
        public void DataBase() {
            var dataSouce = @"D:\Code\Automation\MiddlewareTests\Tests\TestCaseData\SLT1.json";
            _.FetchTestData(dataSouce, "SLT1ValidateRetrieveApplication", "1");
            var connectionString = new DBExecute(_.Environment["DbConnUnsecured"]);
            var result = connectionString.ExecuteQuery(_.TestData["DbQuery"]);
            Assert.IsTrue(result != null);
            var result2 = connectionString.ExecuteQuery(_.TestData["DbQuery"]);
            Assert.IsTrue(result2 != null);
            connectionString.CloseDB();
            Assert.IsTrue(result != null);
        }

        [Test]
        public void DirectoryTest() {
            Console.WriteLine(Assembly.Directory);
        }


        [Test]
        public new void Environment() {
            var testConfigs = new ConfigsBeforeEachTestSuite();
            var env = testConfigs.ReadEnvironmentFromJson(Assembly.Directory + "/Environment.json", "UAT");
            foreach (var keyValuePair in env) {
                Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value);
                if (keyValuePair.Key == "FrontEnd")
                    Assert.AreEqual(keyValuePair.Value, "http://fasteningcode.com/");
            }

            Console.WriteLine(env["DbConnUnsecured"]);
        }

        [Test]
        public void TestSettingsTest() {
            var value = TestSettings["Environment"];
            Console.WriteLine(value);
            Assert.IsTrue(value != null);
        }
#if (DEBUG)
        [Test]
#endif
        public void TestData() {
            _.OpenBrowser();
            _.Driver.Navigate().GoToUrl(_.Environment["FrontEnd2"]);
            _.FetchTestData(@"D:\Projects\Parallel.Test.Framework\Parallel.Test.FrameworkTests\testData1.json", "tc1", "1");
            _.Driver.FindElement(By.Name("q")).SendKeys(_.TestData["SearchQuery"]);
            _.Driver.FindElement(By.Name("q")).SendKeys(Keys.Enter);
            Assert.IsTrue(_.Driver.Title.Contains("fastening"));
            _.CloseBrowser();
        }
    }
}