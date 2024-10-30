using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAutoFramework.TestCases
{
    public class TestPageFormat
    {
        public static List<string> TestLinks { get; set; }

        static TestPageFormat()
        {
            TestLinks = [
                "https://learn.microsoft.com/en-us/python/api/azure-appconfiguration/azure.appconfiguration.azureappconfigurationclient?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-appconfiguration/azure.appconfiguration.azureappconfigurationclient?view=azure-python#azure-appconfiguration-azureappconfigurationclient-list-revisions",
                "https://learn.microsoft.com/en-us/python/api/azure-appconfiguration/azure.appconfiguration.aio.azureappconfigurationclient?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-security-attestation/azure.security.attestation.aio.attestationadministrationclient?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-security-attestation/azure.security.attestation.aio.attestationadministrationclient?view=azure-python",
                "https://learn.microsoft.com/en-us/python/api/azure-security-attestation/azure.security.attestation.aio.attestationclient?view=azure-python#azure-security-attestation-aio-attestationclient-get-signing-certificates"
                ];
        }
    }
}
