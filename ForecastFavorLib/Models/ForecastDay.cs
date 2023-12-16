using Newtonsoft.Json;
using System;
namespace ForecastFavorLib.Models
{
    public class ForecastDay
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("day")]
        public Day Day { get; set; }

        [JsonProperty("hour")]
        public List<Hour> HourlyForecasts { get; set; }

        [JsonProperty("astro")]
        public Astro Astros { get; set; }

    }

    public class Astro
    {
        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }
        [JsonProperty("sunset")]
        public string Sunset { get; set; }
    }
}