using System.Text.Json.Serialization;

namespace DAL.Models
{
    public record City
    {
        [JsonIgnore]
        public int Id { get; init; }
        public required string Name { get; init; }
        public required double Latitude { get; init; }
        public required double Longitude { get; init; }
        public required int Elevation { get; init; }
    }
}
