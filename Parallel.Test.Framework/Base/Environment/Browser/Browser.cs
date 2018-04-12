using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Parallel.Test.Framework.Base.Environment.Browser
{
    public class Browser
    {
        public IWebDriver InitializeBrowser(string assemblyDirectory, string browserName)
        {
            IWebDriver driver = null;
            switch (browserName.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                    chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    //driver = new ChromeDriver(assemblyDirectory + @"\packages\Selenium.Chrome.WebDriver.2.37\driver", chromeOptions);
                    driver = new ChromeDriver(chromeOptions);
                    break;
                case "chromeheadless":
                    var chromeOptions1 = new ChromeOptions();
                    chromeOptions1.AddArgument("--headless");
                    driver = new ChromeDriver(chromeOptions1);
                    break;
                case "firefox":
                    var binary = new FirefoxBinary();
                    var profile = new FirefoxProfile(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    profile.SetPreference("browser.download.folderList", 2);
                    //profile.SetPreference("browser.download.dir", EnvConfig.FileSavePath);//"C\\downloads");
                    profile.SetPreference("browser.helperApps.neverAsk.saveToDisk",
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;"
                        + "application/pdf;"
                        + "application/vnd.openxmlformats-officedocument.wordprocessingml.document;"
                        + "text/plain;"
                        + "text/csv");

#pragma warning disable 618
                    driver = new FirefoxDriver(binary, profile);
#pragma warning restore 618
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
            }

            return driver;
        }
    }
}