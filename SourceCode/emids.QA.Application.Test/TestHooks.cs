using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace eMids.QA.Application.Test
{
    [Binding]
    public class TestHooks
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static readonly string BaseFolderPath = Directory.GetDirectoryRoot(System.IO.Directory.GetCurrentDirectory());
        private static readonly string PathReport = Path.Combine(BaseFolderPath+"ExtentReport", "ExtentReport " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".html");

        [BeforeFeature]
        public static void CreateFeature(FeatureContext featureContext)
        {
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }


        [BeforeTestRun]
        public static void InitializeReport()
        {
            var solutionDir = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));
            var file = Path.Combine(solutionDir, "../..", "Reports", "ExtentReports", "ExtentReport.html");
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);

            var htmlReporter = new ExtentV3HtmlReporter(PathReport);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }


        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else
                        scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else
                        scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else
                        scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
                        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    else
                        scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(scenarioContext.TestError.Message);
            }
        }

        [BeforeScenario]
        public void Initialize(ScenarioContext scenarioContext)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }
    }
}