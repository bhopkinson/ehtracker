using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public static class Syncroniser
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 0 12 * * Fri")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
