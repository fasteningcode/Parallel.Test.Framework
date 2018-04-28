using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using Parallel.Test.Framework.Constants;

namespace Parallel.Test.Framework.Base.Reports
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        static ExtentManager()
        {
            //var reportPath = ResourceConstants.ReportPath;
            var htmlReporter = new ExtentHtmlReporter(TestContext.CurrentContext.TestDirectory + ResourceConstants.ReportPath);
            htmlReporter.Configuration().ChartLocation = ChartLocation.Top;
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            htmlReporter.Configuration().DocumentTitle = "Parallel.Test.Framework";
            htmlReporter.Configuration().ReportName = "Parallel Tests";
            htmlReporter.Configuration().Theme = Theme.Standard;
            Instance.AttachReporter(htmlReporter);
        }

        //private ExtentManager() { }

        public static ExtentReports Instance => _lazy.Value;
    }
}
