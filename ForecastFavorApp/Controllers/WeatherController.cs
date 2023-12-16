using Microsoft.AspNetCore.Mvc;
using ForecastFavorLib.Services;
using System.Threading.Tasks;
using ForecastFavorApp.Models;
using System.Diagnostics;

// This is our WeatherController that deals with weather-related pages
namespace ForecastFavorApp.Controllers
{
    // Inherits from Controller so we get all the MVC controller functionality
    public class WeatherController : Controller
    {
        // This service lets us get weather data
        private readonly IWeatherService _weatherService;
        private readonly string DEF_CITY = "Sudbury";

        // Constructor: .NET gives us an IWeatherService when it makes this controller
        public WeatherController(IWeatherService weatherService)
        {
            // this is DI
            _weatherService = weatherService;
        }

        /// <summary>
        /// Landing Page of weather web app.
        /// It displays the current weather forecast for today.
        /// </summary>
        /// <param name="city">City location</param>
        /// <returns>View with today's forecast</returns>
        public async Task<IActionResult> Index(string city)
        {
            try
            {
                // retrieve city location from user input, session
                if (string.IsNullOrWhiteSpace(city) || city == "null") // no location mentioned 0 
                {
                    // check if we have city in session 
                    var searchCity = HttpContext.Session.GetString("searchCity"); // get from session 

                    if (string.IsNullOrEmpty(searchCity) || searchCity == null) // no location in session 0
                    {
                        // No city is provided, use the default city
                        city = DEF_CITY;
                        HttpContext.Session.SetString("searchCity", city); // save to session

                    }
                    else // yeas location in session  1
                    {
                        city = searchCity;
                    }
                }
                else // location provided 1
                {
                    // save the city in session variable
                    HttpContext.Session.SetString("searchCity", city);
                }

                //fetch all weather data for a city asynchronously.
                var currentWeather = await _weatherService.GetCurrentLocationForecastAsync(city);

                // Check and potentially send a notification
                ViewBag.ConditionMessage = await _weatherService.GetWeatherConditionMessageAsync(city);

                // Send the weather data to the view to be displayed
                return View(currentWeather);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }

        /// <summary>
        /// Tomorrow page of weather web app.
        /// It displays the weather forecast for following day.
        /// </summary>
        /// <param name="city">City location</param>
        /// <returns>View with tomorrow's weather forecast.</returns>
        public async Task<IActionResult> Tomorrow(string city)
        {
            // retrieve city location from user input, session
            if (string.IsNullOrWhiteSpace(city) || city == "null") // no location mentioned 0 
            {
                var sessionCity = HttpContext.Session.GetString("searchCity"); // get from session 
                if (sessionCity == null || string.IsNullOrEmpty(sessionCity)) // no city mentioned 
                {
                    city = DEF_CITY; // set to default value
                                     // save the city in session variable
                    HttpContext.Session.SetString("searchCity", city); // save in session
                }
                else
                {
                    city = sessionCity;
                }
            }

            ViewBag.City = city;
            var weatherForecasts = await _weatherService.GetCurrentLocationForecastAsync(city, 2);
            ViewBag.ConditionMessage = await _weatherService.GetWeatherConditionMessageAsync(city);

            var tom = weatherForecasts.Forecast.ForecastDay.ElementAtOrDefault(1);// just take tomorrows data
            if (tom != null)
            {
                weatherForecasts.Forecast.ForecastDay.Clear();// remove existing items and add just the one you need
                weatherForecasts.Forecast.ForecastDay.Add(tom);
            }

            // Pass the forecast for tomorrow to the view.
            return View(weatherForecasts);
        }

        public async Task<IActionResult> GetMultipleDayForecast(string city)
        {
            var day = 3;

            if (string.IsNullOrWhiteSpace(city) || city == "null") // no location mentioned 0 
            {
                var sessionCity = HttpContext.Session.GetString("searchCity"); // get from session 
                if (sessionCity == null || string.IsNullOrEmpty(sessionCity)) // no city mentioned 
                {
                    city = DEF_CITY; // set to default value
                                     // save the city in session variable
                    HttpContext.Session.SetString("searchCity", city); // save in session
                }
                else
                {
                    city = sessionCity;
                }
            }

            ViewBag.City = city;

            var forecasts = await _weatherService.GetCurrentLocationForecastAsync(city, day);

            return View("MultipleForecast", forecasts);
        }
    }
}
