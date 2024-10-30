using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Legacy;


namespace DataAutoFramework.TestCases
{
    public class TestPageFormat
    {
        public static List<string> TestLinks { get; set; }

        static TestPageFormat()
        {
            TestLinks = [
                "https://learn.microsoft.com/en-us/python/api/azure-ai-language-questionanswering/azure.ai.language.questionanswering.aio.questionansweringclient?view=azure-python"
                ];
        }

        [Test]
        [TestCaseSource(nameof(TestLinks))]
        public async Task TestCodeFormat(string testLink)
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();

            var errorList = new List<string>();

            await page.GotoAsync(testLink);
            
            var elements = (await page.QuerySelectorAllAsync(".lang-python")).Concat(await page.QuerySelectorAllAsync(".has-inner-focus code")).ToArray();
            foreach (var element in elements) {
                var text = await element.InnerHTMLAsync();
                if (text.StartsWith('\n') || text.StartsWith(' ') || text.StartsWith('\t' )) {
                    errorList.Add(text);
                }
            }
            ClassicAssert.Zero(errorList.Count, testLink + " has extra label of  \nErrorInfo:" + string.Join("\nErrorInfo:", errorList));
        }
    }
}
