using AventStack.ExtentReports;
using MemoryZoneFrameworkTest.WebApp.Base;
using MemoryZoneFrameworkTest.WebApp.Helper;
using MemoryZoneFrameworkTest.WebApp.Model;
using MemoryZoneFrameworkTest.WebApp.utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.Tests
{
    public class BaseTest
    {
        [OneTimeSetUp]
        public void ReportSetup()
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = Path.GetFullPath(Path.Combine(projectRoot, @"..\..\..\"));
            string reportsDir = Path.Combine(rootPath, "reports");
            Directory.CreateDirectory(reportsDir);
            string path = Path.Combine(reportsDir, DateTime.Now.ToString("yyyyMMddHHmmss") + ".html");
            ExtentManager.CreateReport(path);
        }
        [SetUp]
        public void Setup()
        {
            CorePage.initDriver(BrowserType.Browser.Chrome);
            ExtentManager.Test = ExtentManager.extentReport.CreateTest(TestContext.CurrentContext.Test.Name);
           

        }
        [OneTimeTearDown]
        public void ReportTearDown()
        {
            ExtentManager.FlushReport();
        }
        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TakeScreenshot(Status.Fail, "Test failed" + stacktrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                TakeScreenshot(Status.Pass, "Test Pass " + stacktrace);
            }
            if (CorePage.driver != null)
            {
                CorePage.closeDriver();
            }
        }
        public void TakeScreenshot(Status status, string message)
        {
            string fileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = Path.GetFullPath(Path.Combine(projectRoot, @"..\..\..\"));
            string screenDir = Path.Combine(rootPath, "screenshot");
            Directory.CreateDirectory(screenDir);
            string fullPath = Path.Combine(screenDir, fileName);
          

            Screenshot sc = ((ITakesScreenshot)CorePage.driver).GetScreenshot();
            File.WriteAllBytes(fullPath, sc.AsByteArray);

            ExtentManager.Test.Log(status, message,
                MediaEntityBuilder.CreateScreenCaptureFromPath(fullPath).Build());
        }
        
        public static IEnumerable<AppEntity> LoginWithJsonDataSource()
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = Path.GetFullPath(Path.Combine(projectRoot, @"..\..\..\"));
            string jsonPath = Path.Combine(rootPath, "Data", "login.json");
            var jsonString = File.ReadAllText(jsonPath);

            var loginModel = JsonSerializer.Deserialize<List<AppEntity>>(jsonString);
            foreach(var data in loginModel)
            {
                yield return data;
            }
            
        }

        
        
    }
}
