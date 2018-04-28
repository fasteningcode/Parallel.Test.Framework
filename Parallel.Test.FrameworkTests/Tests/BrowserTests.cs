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
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/h1")).Text.Contains("Secret Login Page"));
            _.CloseBrowser(driver);
        }
    }
}
