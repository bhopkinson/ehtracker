using Microsoft.WindowsAzure.Storage.Table;

namespace Functions.Models
{
    public class Property : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Path { get; set; }
        public string ImagePath { get; set; }
        public GeoCoordinate Location { get; set; }
        public bool IsCurrent { get; set; }
    }
}
