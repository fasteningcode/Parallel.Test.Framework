using System.Collections.Generic;
using System.Diagnostics;
using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Parallel.Test.Framework.Base.Reports;
using Parallel.Test.Framework.Base.TestSettings;
using Parallel.Test.Framework.Constants;
using Parallel.Test.FrameworkTests;

namespace Parallel.Test.Framework.Base
{
    public class Base
    {
        public BeforeEachTestBase _;
        public Dictionary<string, string> TestSettings { get; set; }
        public Dictionary<string, string> Environment { get; set; }

        //public enum RunTest {
        //    Local,
        //    BrowserStatck
        //}

        [OneTimeSetUp]
        public void OneTimeSetupTestSuite()
        {
            var configsSuite = new ConfigsBeforeEachTestSuite();
            var AssemblyDirectory = Assembly.Directory;
            TestSettings = configsSuite.TestSetup(AssemblyDirectory + "/TestSettings.json");
            Environment = configsSuite.ReadEnvironmentFromJson(Assembly.Directory + "/Environment.json", TestSettings[TestSettingsConst.ENVIRONMENT]);

            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [OneTimeTearDown]
        public void OneTimeTearDownTestSuite()
        {
            ExtentManager.Instance.Flush();
            if(TestSettings[TestSettingsConst.RUNTEST]==RunTest.Local.ToString())
                Process.Start(TestContext.CurrentContext.TestDirectory + ResourceConstants.ReportPath);
        }


        [SetUp]
        public void SetupTest()
        {
            _ = new BeforeEachTestBase(TestSettings, Environment);
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDownTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
        }
    }
}