using System;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using Parallel.Test.Framework.Base.Reports;

namespace Parallel.Test.Framework.Lib.Nunit {
    public class AssertionHelpers {
        public void AreEqual(string expected, string actual, string customMessage) {
            try {
                Assert.AreEqual(expected.ToLower(), actual.ToLower());
                ExtentTestManager.GetTest().Log(Status.Info, customMessage + " Validation Passed\"" + "\",  Expetcted(Test Data Source) : \"" + expected + "\"" + "Actual : \"" + actual + "\"");
            }
            catch (Exception e) {
                ExtentTestManager.GetTest().Log(Status.Error, "Assertion Failed,\"" + customMessage + "\",  Expected(Test Data Source) :  \"" + expected + " \"" + "Actual :  " + actual + e.Message);
            }
        }

        public void That(IWebElement element, string message) {
            try {
                Assert.That(element.Displayed);
                ExtentTestManager.GetTest().Log(Status.Pass, message + "is Displayed");
            }
            catch (Exception) {
                ExtentTestManager.GetTest().Log(Status.Error, message + "not is Displayed");
            }
        }
    }
}