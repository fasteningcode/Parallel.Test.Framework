using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using Parallel.Test.Framework.Constants;

namespace Parallel.Test.Framework.Base.Reports
{
    public static class Report
    {

        public static string Capture(this IWebDriver driver, string screenShotFileName = "")
        {
            var folderLocation = TestContext.CurrentContext.TestDirectory + ResourceConstants.ScreenshotPath;

            //Creating the directory if it didnt exists
            if (!Directory.Exists(folderLocation)) Directory.CreateDirectory(folderLocation);
            var fileName = new StringBuilder(folderLocation);

            //fileName.Append(screenShotFileName);
            fileName.Append(TestContext.CurrentContext.Test.Name + screenShotFileName);
            fileName.Append(DateTime.Now.ToString("yyyy_MM_dd_T_HH_mm_ss_ff"));
            fileName.Append(".png");

          
            //Selenium Method for taking screenshot
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
            screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Png);
            var uri = new Uri(fileName.ToString());
            return uri.AbsoluteUri;
            //return fileName.ToString();
        }

    }
}
