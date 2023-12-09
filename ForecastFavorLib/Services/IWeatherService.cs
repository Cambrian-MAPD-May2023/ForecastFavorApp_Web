using ForecastFavorLib.Models;
using ForecastFavorLib.Models.APIModels;
using System.Threading.Tasks;

// This namespace is like a container for all our service-related stuff.
namespace ForecastFavorLib.Services
{
    // IWeatherService is like a promise of what a weather service needs to do.
    public interface IWeatherService
    {
        // This method should get us the current weather for a given location.
        Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string location);

        // This one should get us the weather forecast for a location, for a certain number of days ahead.
        Task<ForecastResponse> GetForecastAsync(string location, int days);

        /// <summary>
        /// Returns LOCATION, CURRENT MOMENT FORECAST AND WHOLE DAY FORECAST data 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        Task<CurrentLocationForecastResponse> GetCurrentLocationForecastAsync(string location, int days = 0);
    }
}