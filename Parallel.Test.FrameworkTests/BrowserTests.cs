using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Parallel.Test.FrameworkTests
{
    [TestFixture]
    [Parallelizable]
    public class BrowserTests : Framework.Base.Base
    {
        [Test]
        public void OpenBrowser()
        {
            _.OpenBrowser();
            _.Driver.Navigate().GoToUrl(_.Environment["FrontEnd"]);
            Console.WriteLine(_.Driver.Title);
            Assert.IsTrue(_.Driver.Title.Contains("Fastening"));
            _.CloseBrowser();
        }
    }
}
