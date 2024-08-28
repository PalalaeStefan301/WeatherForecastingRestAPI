using System.Text.Json.Serialization;

namespace WeatherForecastingRestAPI.Models
{
    [Flags]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum EnumTemperatureTypes
    {
        Celsius = 0,
        Fahrenheit = 1,
    }
}