using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Functions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;

namespace Functions
{
    public static class Syncroniser
    {
        private static HttpClient httpClient = new HttpClient();

        [FunctionName("SyncroniseProperties")]
        public static async Task Run(
            [TimerTrigger("0 0 12 * * Fri")]TimerInfo myTimer,
            [Table("Properties")]CloudTable propertiesTable,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var propertiesString = await httpClient.GetStringAsync("https://www.english-heritage.org.uk/api/PropertySearch/GetAll");
            await SyncroniseProperties(propertiesString, propertiesTable);
        }

        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", "sync")]HttpRequest req,
            [Table("Properties")]CloudTable propertiesTable,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            await SyncroniseProperties(requestBody, propertiesTable);
        }

        private static async Task SyncroniseProperties(string propertiesSource, CloudTable propertiesTable)
        {
            var sourceProperties = JObject.Parse(propertiesSource)["Results"].Select(p =>
                            new Property
                            {
                                Id = (int)p["ID"],
                                Name = (string)p["Title"],
                                Summary = (string)p["Summary"],
                                Path = (string)p["Path"],
                                ImagePath = (string)p["ImagePath"],
                                Location = new GeoCoordinate
                                {
                                    Latitude = (double)p["Latitude"],
                                    Longitude = (double)p["Longitude"]
                                },
                                IsCurrent = true
                            }).ToList();

            var propertiesQuerySegment = await propertiesTable.ExecuteQuerySegmentedAsync(new TableQuery<Property>(), null);
            var propertiesDictionary = propertiesQuerySegment.ToDictionary(p => p.Id, p => p);

            var addedProperties = new List<Property>();
            var updatedProperties = new List<Property>();

            foreach (var sourceProperty in sourceProperties)
            {
                if (!propertiesDictionary.TryGetValue(sourceProperty.Id, out var destinationProperty))
                {
                    // New property
                    addedProperties.Add(sourceProperty);
                }

                
            }
        }
    }
}
