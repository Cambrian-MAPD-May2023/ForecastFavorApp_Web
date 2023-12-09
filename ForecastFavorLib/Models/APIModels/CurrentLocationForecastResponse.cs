using Newtonsoft.Json;

namespace ForecastFavorLib.Models.APIModels
{
    public class CurrentLocationForecastResponse
    {

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        [JsonProperty("forecast")]
        public Forecast Forecast { get; set; }


    }
}