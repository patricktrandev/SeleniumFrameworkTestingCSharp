using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.WebApp.Helper
{
    public class ExtentManager
    {
        public static ExtentReports extentReport;
        //public static ExtentTest Test;
        public static ExtentTest Step;
        private static ThreadLocal<ExtentTest> test = new ThreadLocal<ExtentTest>();

        public static ExtentTest Test
        {
            get { return test.Value; }
            set { test.Value = value; }
        }
        public static void CreateReport(string path)
        {
            
            extentReport = new ExtentReports();
            var html = new ExtentSparkReporter(@path);

            html.Config.DocumentTitle = "Test Execution Report";
            html.Config.DocumentTitle = "MemoryZone Automation Framework Test";
            html.Config.ReportName = "MemoryZone Framework Test";
            html.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
            extentReport.AttachReporter(html);

            
            extentReport.AddSystemInfo("OS", Environment.OSVersion.ToString());
            extentReport.AddSystemInfo("Tester", "Tientn16");
        }

        public static void FlushReport()
        {
            extentReport.Flush();
        }

        
    }
}
