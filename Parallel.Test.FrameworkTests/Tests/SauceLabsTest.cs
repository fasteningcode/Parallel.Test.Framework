using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Parallel.Test.FrameworkTests.Tests
{
    [TestFixture("chrome", "45", "Windows 7", "", "")]
    public class SauceNUnitTest
    {
        private IWebDriver _driver;
        private String _browser;
        private String _version;
        private String _os;
        private String _deviceName;
        private String _deviceOrientation;

        public SauceNUnitTest(String browser, String version, String os, String deviceName, String deviceOrientation)
        {
            _browser = browser;
            _version = version;
            _os = os;
            _deviceName = deviceName;
            _deviceOrientation = deviceOrientation;
        }

        [SetUp]
        public void Init()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability(CapabilityType.BrowserName, _browser);
            caps.SetCapability(CapabilityType.Version, _version);
            caps.SetCapability(CapabilityType.Platform, _os);
            caps.SetCapability("deviceName", _deviceName);
            caps.SetCapability("deviceOrientation", _deviceOrientation);
            caps.SetCapability("username", "aadhithbose");
            caps.SetCapability("accessKey", "1cc813ac-bec2-4dd8-9e9e-a239ec2e7c2c");
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

            _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));


        }

#if (!DEBUG)
        [Test]
#endif
        public void GoogleTest()
        {
            _driver.Navigate().GoToUrl("http://www.google.com");
            StringAssert.Contains("Google", _driver.Title);
            IWebElement query = _driver.FindElement(By.Name("q"));
            query.SendKeys("Sauce Labs");
            query.Submit();
        }

        [TearDown]
        public void CleanUp()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            try
            {
                // Logs the result to Sauce Labs
                ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // Terminates the remote webdriver session
                _driver.Quit();
            }
        }
    }
}