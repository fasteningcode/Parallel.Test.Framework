using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Parallel.Test.FrameworkTests.Tests
{
    [TestFixture]
    [Parallelizable]
    public class BrowserTests : Framework.Base.Base
    {
#if (!DEBUG)
        [Test]
#endif
        public void OpenBrowser()
        {
            IWebDriver driver = _.OpenBrowser();
            driver.Navigate().GoToUrl(_.Environment["FrontEnd"]);
            Console.WriteLine(driver.Title);
            Assert.IsTrue(driver.Title.Contains("Fastening"));
            _.CloseBrowser(driver);
        }
    }
}
