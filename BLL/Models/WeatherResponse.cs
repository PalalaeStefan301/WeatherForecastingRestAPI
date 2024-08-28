
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
    }
}
