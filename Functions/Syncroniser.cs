using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Functions.Models;
using Microsoft.Azure.WebJobs;
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
        public static async void Run(
            [TimerTrigger("0 0 12 * * Fri")]TimerInfo myTimer,
            [Table("Properties")]CloudTable propertiesTable,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var responseString = await httpClient.GetStringAsync("https://www.english-heritage.org.uk/api/PropertySearch/GetAll");
            var ehProperties = JObject.Parse(responseString)["Results"].Select(p =>
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

            var propertiesToAdd = new List<Property>();

            foreach (var ehProperty in ehProperties)
            {
                if (!propertiesDictionary.TryGetValue(ehProperty.Id, out var property))
                {
                    propertiesToAdd.Add(ehProperty);
                }
            }
        }
    }
}
