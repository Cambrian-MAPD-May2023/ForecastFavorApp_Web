using ForecastFavorLib.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


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
           public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
             // Create a new HttpClient using the factory
            _httpClient = httpClientFactory.CreateClient();
            // Retrieve the API key from IConfiguration 
            _apiKey = configuration["WeatherAPI:ApiKey"] ?? throw new InvalidOperationException("API key not found in configuration");
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
            // Fetch the current weather data for the specified location.
            var currentWeather = await GetCurrentWeatherAsync(location);
            
            // Check if the current weather condition includes "rain" and return a message for rainy weather.
            if (currentWeather.Current.Condition.Text.Contains("rain"))
            {
                return "It's rainy today. Don't forget to carry an umbrella!";
            }
            // Check if the current weather condition includes "Sunny" and return a message for sunny weather.
            else if (currentWeather.Current.Condition.Text.Contains("Sunny"))
            {
                return "It's sunny today. A perfect day for outdoor activities!";
            }
            // Check if the current weather condition includes "cloudy" and return a message for cloudy weather.
            else if (currentWeather.Current.Condition.Text.Contains("cloudy"))
            {
                return "It's cloudy today. You might need a light jacket.";
            }
            // Check if the current weather condition includes "snow" and return a message for snowy weather.
            else if (currentWeather.Current.Condition.Text.Contains("snow"))
            {
                return "It's snowy today. Stay warm and drive safely!";
            }

            // Default message for cases where the weather condition is not identified or doesn't match any of the above.
            return "Current weather condition is not identified.";
        }
    }
}
