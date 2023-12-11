using ForecastFavorLib.Models;
using ForecastFavorLib.Data;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;



// Our services go in this namespace.
namespace ForecastFavorLib.Services
{
    // WeatherService is the class where we actually go out and get the weather data.
    public class WeatherService : IWeatherService
    {
        // We use _httpClient to talk to the internet.
        private readonly HttpClient _httpClient;
        // We need an API key to ask WeatherAPI.com for data.
        private readonly string _apiKey; 
  
        // When we make a new WeatherService, we need to give it an HttpClient and an API key.
       private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly AppDbContext _context;

        public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHubContext<NotificationHub> notificationHub, AppDbContext context)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["WeatherAPI:ApiKey"] ?? throw new InvalidOperationException("API key not found in configuration");
            _notificationHub = notificationHub;
            _context = context;
        }


        // This method gets the current weather for a specific place.
        public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string location)
        {
            // We can't look up the weather for "nowhere," so we check if the location is empty or null.
            if (string.IsNullOrEmpty(location))
                throw new ArgumentException("Location cannot be null or empty.", nameof(location));

            // We build the URL to ask for the current weather data.
            var url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={location}&aqi=no";
            // We ask the internet for the weather data and wait for the response.
            var response = await _httpClient.GetAsync(url);
            // If something went wrong with our request, we throw an error.
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching current weather: {response.StatusCode}");
            }

            // We read the data we got and turn it into text.
            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Then we turn that text into a CurrentWeatherResponse object.
            var currentWeatherResponse = JsonConvert.DeserializeObject<CurrentWeatherResponse>(jsonResponse);
            
            // If we didn't get proper data back, we throw an error.
            if (currentWeatherResponse == null)
            {
                throw new InvalidOperationException("Unable to deserialize the current weather data.");
            }

            // If everything went well, we return the current weather data.
            return currentWeatherResponse;
        }

        // This method gets the weather forecast for a specific place and number of days.
        public async Task<ForecastResponse> GetForecastAsync(string location, int days)
        {
            // Just like with current weather, we check if the location is valid.
            if (string.IsNullOrEmpty(location))
                throw new ArgumentException("Location cannot be null or empty.", nameof(location));

            // We build the URL to ask for the forecast data.
            var url = $"http://api.weatherapi.com/v1/forecast.json?key={_apiKey}&q={location}&days={days}&aqi=no&alerts=no";
            // We ask the internet for the forecast data and wait for the response.
            var response = await _httpClient.GetAsync(url);
            // If something went wrong with our request, we throw an error.
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching weather forecast: {response.StatusCode}");
            }

            // We read the data we got and turn it into text.
            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Then we turn that text into a ForecastResponse object.
            var forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(jsonResponse);

            // If we didn't get proper data back, we throw an error.
            if (forecastResponse == null)
            {
                throw new InvalidOperationException("Unable to deserialize the forecast data.");
            }

            // If everything went well, we return the forecast data.
            return forecastResponse;
        }


        /// Asynchronously gets a personalized weather condition message for a specified location.
       public async Task<string> GetWeatherConditionMessageAsync(string location)
        {
            // Retrieve the current weather conditions for the given location
            var currentWeather = await GetCurrentWeatherAsync(location);

            // Determine the weather condition message based on the current weather conditions
            var conditionMessage = DetermineConditionMessage(currentWeather.Current.Condition.Text);

            // Fetch the default user
            var defaultUser = _context.Users.Include(u => u.Preferences).FirstOrDefault(u => u.UserID == 1);

            // Check if the default user should be notified
            if (defaultUser != null && ShouldNotifyUser(defaultUser, currentWeather.Current.Condition.Text))
            {
                // Send a notification to the default user via SignalR
                await _notificationHub.Clients.All.SendAsync("ReceiveNotification", conditionMessage);
            }

            return conditionMessage;
        }


        // Determine the appropriate weather condition message based on the condition text
        private string DetermineConditionMessage(string conditionText)
        {
            if (string.IsNullOrWhiteSpace(conditionText))
            {
                return "Weather condition is not available.";
            }

            if (conditionText.Contains("rain", StringComparison.OrdinalIgnoreCase))
            {
                return "It's rainy today. Don't forget to carry an umbrella!";
            }
            else if (conditionText.Contains("sunny", StringComparison.OrdinalIgnoreCase))
            {
                return "It's sunny today. A perfect day for outdoor activities!";
            }
            else if (conditionText.Contains("cloudy", StringComparison.OrdinalIgnoreCase))
            {
                return "It's cloudy today. You might need a light jacket.";
            }
            else if (conditionText.Contains("snow", StringComparison.OrdinalIgnoreCase))
            {
                return "It's snowy today. Stay warm and drive safely!";
            }
            else
            {
                // Default message for conditions that are not explicitly handled above
                return $"The weather is {conditionText} today.";
            }
        }

        // Determine if a user should be notified based on their preferences and the weather condition
        private bool ShouldNotifyUser(User user, string conditionText)
        {
            // Ensure conditionText is valid
            if (string.IsNullOrWhiteSpace(conditionText))
            {
                return false;
            }

            // Ensure user has preferences set
            if (user.Preferences == null)
            {
                return false;
            }

            // Check for rain condition
            if (conditionText.Contains("rain", StringComparison.OrdinalIgnoreCase) && user.Preferences.NotifyOnRain)
            {
                return true;
            }

            // Check for sunny condition
            if (conditionText.Contains("sunny", StringComparison.OrdinalIgnoreCase) && user.Preferences.NotifyOnSun)
            {
                return true;
            }

            // Check for cloudy condition
            if (conditionText.Contains("cloudy", StringComparison.OrdinalIgnoreCase) && user.Preferences.NotifyOnClouds)
            {
                return true;
            }

            // Check for snow condition
            if (conditionText.Contains("snow", StringComparison.OrdinalIgnoreCase) && user.Preferences.NotifyOnSnow)
            {
                return true;
            }

            // If none of the conditions are met, return false
            return false;
        }


    }
}
