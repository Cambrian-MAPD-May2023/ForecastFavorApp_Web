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
    private readonly string DEF_CITY = "Sudbury";// _weatherService;

    // Constructor: .NET gives us an IWeatherService when it makes this controller
    public WeatherController(IWeatherService weatherService)
    {
        // Save the service so we can use it later
        _weatherService = weatherService;
    }

    // The main page of our weather section
    public async Task<IActionResult> Index(string city)
    {
        try
        {
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


            var currentWeather = await _weatherService.GetCurrentLocationForecastAsync(city);

            // var currentWeather = await _weatherService.GetCurrentWeatherAsync(city);

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


    public async Task<IActionResult> Tomorrow(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            // No city is provided, use the default city
            city = "Sudbury, Ontario, Canada ";
        }
        ViewBag.City = city;

        // Fetch the forecast for the next 2 days including today for the specified city.
        var forecast = await _weatherService.GetForecastAsync(city, 2);

        // Select the forecast for tomorrow which should be the second element of the forecastday array.
        var tomorrowForecast = forecast.Forecast.ForecastDay.ElementAtOrDefault(1);

        // Check if tomorrow's forecast is available.
        if (tomorrowForecast == null)
        {
            // Handle the case where tomorrow's forecast is not available.
            return View("Error");
        }

        // Pass the forecast for tomorrow to the view.
        return View(tomorrowForecast);
    }

    public async Task<IActionResult> GetMultipleDayForecast()
    {
        var day = 3;
        @ViewBag.City = "Sudbury";
        var forecasts = await _weatherService.GetCurrentLocationForecastAsync("Sudbury", day);

        return View("MultipleForecast",forecasts);
    }
}
}
