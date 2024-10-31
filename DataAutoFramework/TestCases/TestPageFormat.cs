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
                "https://learn.microsoft.com/en-us/python/api/overview/azure/cosmos-readme?view=azure-python#public-preview---vector-search",
                "https://learn.microsoft.com/en-us/python/api/azure-data-tables/azure.data.tables?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-appconfiguration/azure.appconfiguration.azureappconfigurationclient?view=azure-python#azure-appconfiguration-azureappconfigurationclient-list-revisions",
                "https://learn.microsoft.com/en-us/python/api/azure-security-attestation/azure.security.attestation.aio.attestationadministrationclient?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/overview/azure/ai-contentsafety-readme?view=azure-python#analyze-text-without-blocklists",
                "https://learn.microsoft.com/en-us/python/api/azure-ai-formrecognizer/azure.ai.formrecognizer.aio.documentanalysisclient?view=azure-python#examples",
                "https://learn.microsoft.com/en-us/python/api/azure-ai-language-conversations/azure.ai.language.conversations.aio.conversationanalysisclient?view=azure-python#azure-ai-language-conversations-aio-conversationanalysisclient-analyze-conversation",
                "https://learn.microsoft.com/en-us/python/api/azure-ai-language-questionanswering/azure.ai.language.questionanswering.aio.questionansweringclient?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-ai-textanalytics/azure.ai.textanalytics.aio.asyncanalyzehealthcareentitieslropoller?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-ai-translation-document/azure.ai.translation.document.aio.documenttranslationclient?view=azure-python-preview#examples",
                "https://learn.microsoft.com/en-us/python/api/azure-communication-chat/azure.communication.chat.aio.chatclient?view=azure-python#examples",
                "https://learn.microsoft.com/en-us/python/api/azure-communication-jobrouter/azure.communication.jobrouter.aio.jobrouterclient?view=azure-python#azure-communication-jobrouter-aio-jobrouterclient-cancel-job",
                "https://learn.microsoft.com/en-us/python/api/overview/azure/cognitive-services?view=azure-python#computer-vision",
                "https://learn.microsoft.com/en-us/python/api/overview/azure/security-attestation-readme?view=azure-python#set-an-attestation-policy-for-a-specified-attestation-type",
                "https://learn.microsoft.com/en-us/python/api/overview/azure/ai-language-questionanswering-readme?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-ai-language-questionanswering/azure.ai.language.questionanswering.authoring.aio.authoringclient?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-identity/azure.identity?view=azure-python#classes"
                ];
            //TestLinks = [
            //    "https://learn.microsoft.com/en-us/python/api/azure-data-tables/azure.data.tables?view=azure-python"
            //    ];
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

            //var elements = (await page.QuerySelectorAllAsync(".lang-python")).Concat(await page.QuerySelectorAllAsync(".has-inner-focus code")).ToArray();
            var elements = await page.QuerySelectorAllAsync("[class^='lang-'], [class*=' lang-']");
            foreach (var element in elements) {
                var text = await element.InnerTextAsync();
                if (text.StartsWith('\n') || text.StartsWith(' ') || text.StartsWith('\t' )) {
                    errorList.Add(text);
                }else
                {
                    Console.WriteLine(text);
                }
            }
            ClassicAssert.Zero(errorList.Count, testLink + " has extra label of  \nErrorInfo:" + string.Join("\nErrorInfo:", errorList));
        }
    }
}
