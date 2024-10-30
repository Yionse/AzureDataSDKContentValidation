using NUnit.Framework.Legacy;
using NUnit.Framework;
using Microsoft.Playwright;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace DataAutoFramework.TestCases
{
    public class TestPageSymbol
    {
        public static List<string> TestLinks { get; set; }

        static TestPageSymbol()
        {
            TestLinks = [
              "https://learn.microsoft.com/en-us/python/api/azure-ai-ml/azure.ai.ml.sweep.sweepjob?view=azure-python",
              "https://learn.microsoft.com/en-us/python/api/azure-functions-durable/azure.durable_functions.models.durableorchestrationstatus.durableorchestrationstatus?view=azure-python",
              "https://learn.microsoft.com/en-us/python/api/azure-functions-durable/azure.durable_functions.models.tokensource.tokensource?view=azure-python",
              "https://learn.microsoft.com/en-us/python/api/azure-functions-durable/azure.durable_functions.tasks?view=azure-python#azure-durable-functions-tasks-call-sub-orchestrator-task",
              "https://learn.microsoft.com/en-us/python/api/azure-eventhub/azure.eventhub.eventhubconnectionstringproperties?view=azure-python#azure-eventhub-eventhubconnectionstringproperties-endpoint",
              "https://learn.microsoft.com/en-us/python/api/azure-storage-file-share/azure.storage.fileshare.services?view=azure-python",
              "https://learn.microsoft.com/en-us/python/api/azure-ai-contentsafety/azure.ai.contentsafety.models.imagecategoriesanalysis?view=azure-python"
              ];
        }


        [Test]
        [TestCaseSource(nameof(TestLinks))]
        public async Task TestExtraLabel(string testLink)
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            char[] chars = ['<', '>', '[', ']', '/', '~'];
            var errorList = new List<string>();

            await page.GotoAsync(testLink);
            //var elements = await page.QuerySelectorAllAsync(".lang-python");
            var elements = (await page.QuerySelectorAllAsync(".content div p")).Concat(await page.QuerySelectorAllAsync(".content div span")).ToArray();
            foreach ( var element in elements )
            {
                var text = await element.InnerTextAsync();
                if (text.IndexOfAny(chars) >= 0 && !text.Contains("https://") && !text.Contains("http://"))
                {
                    errorList.Add(text);
                    Console.WriteLine(text, "-=-=-=-==-=-=-=");
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
            ClassicAssert.Zero(errorList.Count, testLink + " has extra label of  \nErrorInfo:" + string.Join("\nErrorInfo:", errorList));

        }

        [Test]
        [TestCaseSource(nameof(TestLinks))]
        public async Task TestExtraLabelInTable(string testLink)
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            var errorList = new List<string>();

            await page.GotoAsync(testLink);
            var elements = await page.QuerySelectorAllAsync(".content table div.subtle");
            foreach (var element in elements)
            {
                string text = await element.InnerTextAsync();
                if (text.Equals(">") || text.Equals("<"))
                {
                    errorList.Add(text);
                }
            }
            ClassicAssert.Zero(errorList.Count, testLink + " has extra label of  \nErrorInfo:" + string.Join("\nErrorInfo:", errorList));
        }

        [Test]
        [TestCaseSource(nameof(TestLinks))]
        public async Task TestExtraLabelForInCode(string testLink)
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            var errorList = new List<string>();

            await page.GotoAsync(testLink);
            var elements = await page.QuerySelectorAllAsync(".lang-python");
            foreach (var element in elements)
            {
                var text = await element.InnerTextAsync();
                if (text.Contains('~'))
                {
                    errorList.Add(text);
                }
            }
            ClassicAssert.Zero(errorList.Count, testLink + " has extra label of  \nErrorInfo:" + string.Join("\nErrorInfo:", errorList));

        }
    }
}
