using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Parallel.Test.Framework.Constants;

namespace Parallel.Test.Framework.Base.Environment.Browser
{
    public class Browser
    {
        public IWebDriver InitializeBrowser(Dictionary<string, string> testSettings) {
            var browserName = testSettings[TestSettingsConst.BROWSER];
            var executingAssembly = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            IWebDriver driver = null;
            switch (browserName.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                    //chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    chromeOptions.AddUserProfilePreference("download.default_directory", TestContext.CurrentContext.TestDirectory + ResourceConstants.ReportPath);
                    driver = new ChromeDriver(executingAssembly,chromeOptions);
                    break;
                case "chromeheadless":
                    var chromeOptions1 = new ChromeOptions();
                    chromeOptions1.AddArgument("--headless");
                    driver = new ChromeDriver(executingAssembly,chromeOptions1);
                    break;
                case "firefox":
                    //var binary = new FirefoxBinary();
                    //var profile = new FirefoxProfile(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    //profile.SetPreference("browser.download.folderList", 2);
                    //profile.SetPreference("browser.download.dir", EnvConfig.FileSavePath);//"C\\downloads");
                    //profile.SetPreference("browser.helperApps.neverAsk.saveToDisk",
                    //    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;"
                    //    + "application/pdf;"
                    //    + "application/vnd.openxmlformats-officedocument.wordprocessingml.document;"
                    //    + "text/plain;"
                    //    + "text/csv");

                    ////#pragma warning disable 618
                    ////                    driver = new FirefoxDriver(binary, profile);
                    ////#pragma warning restore 618
                    driver = new FirefoxDriver(executingAssembly);
                    break;
                case "firefoxheadless":
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--headless");
                    driver = new FirefoxDriver(executingAssembly, firefoxOptions);
                    break;
                case "internetexplorer":
                    driver = new InternetExplorerDriver(executingAssembly);
                    break;
                case "edge":
                    
                    driver = new EdgeDriver(executingAssembly);
                    break;
            }

            return driver;
        }
    }
}