
using BLL.Models;
using DAL.Models;
using System.Text.Json.Serialization;

namespace WeatherForecastingRestAPI.Models
{
    public record WeatherResponse
    {
        public required double Temperature { get; init; }
        public required City City { get; init; }
        public required DateTime CurrentTime { get; init; }
        public required EnumTimezones Timezone { get; init; }
        public required int Humidity { get; init; }
        public required int Precipitation { get; init; }
        public required int RainChance { get; init; }
        public required double UvIndex { get; init; }
    }
}
