using System;
using System.Threading;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Parallel.Test.Framework.Base.Reports;

namespace Parallel.Test.Framework.Lib.Selenium {
    public class WebElementExtensions {
        public IWebElement WaitForElement(IWebDriver driver, IWebElement element, TimeSpan timeSpan = default(TimeSpan)) {
            //try {
            var wait = new WebDriverWait(driver, timeSpan);
#pragma warning disable 618
            return wait.Until(ExpectedConditions.ElementToBeClickable(element));
#pragma warning restore 618
            //}
            //catch (Exception e) {
            //    ExtentTestManager.GetTest().Log(Status.Warning, "Exception in Wait for Element " + e.Message);
            //}
        }

        public void SendKeys(IWebElement element, string value, string txtBoxName = null) {
            //try {
            element.Clear();
            element.SendKeys(value);
            ExtentTestManager.GetTest().Log(Status.Info, "Entered the " + txtBoxName + " as \"" + value + "\"");
            //}
            //catch (Exception e) {
            //    ExtentTestManager.GetTest().Log(Status.Error, "Error in SendKeys " + txtBoxName + " as \"" + value + " \"" + e.Message);
            //}
        }

        public void SelectDropDownList(IWebElement element, string value, string description = null) {
            //  try {
            var ddl = new SelectElement(element);
            ddl.SelectByText(value);
            ddl.SelectedOption.Click();
            ExtentTestManager.GetTest().Log(Status.Info, "Selected \"" + value + "\"" + " from the Dropdown List for  " + description);
            //}
            //catch (Exception e) {
            //    ExtentTestManager.GetTest().Log(Status.Error, "Error in Selecting drop down list  \"" + value + "\"" + " from the Dropdown List for  " + description + e.Message);
            //}

            ExtentTestManager.GetTest().Log(Status.Info, description + "  " + value);
        }

        public void Click(IWebElement element, string name) {
            //try {
            element.Click();
            ExtentTestManager.GetTest().Log(Status.Info, "Clicked the \"" + name + " \"");
            //}
            //catch (Exception e) {
            //    ExtentTestManager.GetTest().Log(Status.Error, "Exception in Click element \"" + name + " \"" + e.Message);
            //}
        }

        public string Text(IWebElement element, string name = null) {
            string rtn;

            //  try {
            rtn = element.Text;
            ExtentTestManager.GetTest().Log(Status.Info, "\"" + name + "\"  is \"" + rtn + "  \"");
            // }
            //catch (Exception e) {
            //    ExtentTestManager.GetTest().Log(Status.Error, "Exception in Text" + "\"" + name + "\"  is \"" + rtn + "  \"" + e.Message);
            //}

            return rtn;
        }

        public void ImplicitWait(int i) {
            Thread.Sleep(i * 100);
        }
    }
}