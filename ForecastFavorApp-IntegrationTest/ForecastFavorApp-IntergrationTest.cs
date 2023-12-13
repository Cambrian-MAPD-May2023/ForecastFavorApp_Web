using ForecastFavorLib.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ForecastFavorApp_IntegrationTest
{
    [TestClass]
    public class IntegrationTest
    {
        private IWebDriver _webDriver;
        private HttpClient _httpClient;

        [TestInitialize]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var opts = new ChromeOptions();
            _webDriver = new ChromeDriver(opts);
            _httpClient = new HttpClient();
        }
        //Asserting the title
        [TestMethod]
        public void Test()
        {
            var baseUrl = "https://localhost:7293/";
            _webDriver.Navigate().GoToUrl(baseUrl);
            Assert.IsTrue(_webDriver.Title.Contains("ForecastFavorApp"), $"Expected title not found on {baseUrl}");
        }
        //Asserting the today tab title
        [TestMethod]
        public void TodayTabTest()
        {
            var baseUrl = "https://localhost:7293/";
            _webDriver.Navigate().GoToUrl(baseUrl);
            var todayTab = _webDriver.FindElement(By.CssSelector(".nav-item"));
            Assert.AreEqual("Today", todayTab.Text);
        }
        //Asserting the tomorrow tab title
        [TestMethod]
        public void TomorrowTabTest()
        {
            var baseUrl = "https://localhost:7293/";
            _webDriver.Navigate().GoToUrl(baseUrl);
            var tomorrowTab = _webDriver.FindElement(By.LinkText("Tomorrow"));
            Assert.AreEqual("Tomorrow", tomorrowTab.Text);
        }
        //Asserting the Temperature, Condition, Pressure, Humidity, Cloud Cover, Feels Like, Wind Speed data from the api with the web in the today page
        [TestMethod]
        public async Task ApiDataAndWebDataForTodayPageIntegrationTest()
        {   
         try
            {
                var apiKey = "bcea99f817174902bdc03259230311"; 
                var apiEndpoint = "http://api.weatherapi.com/v1/forecast.json?key=" + apiKey + "&q=sudbury&days=1&aqi=no&alerts=no";
                var apiResponse = await _httpClient.GetStringAsync(apiEndpoint);
                var apiData = JsonConvert.DeserializeObject<CurrentWeatherResponse>(apiResponse);
                var baseUrl = "https://localhost:7293/Weather";
                _webDriver.Navigate().GoToUrl(baseUrl);
                var temperatureOnWebPage = _webDriver.FindElement(By.CssSelector(".current-weather .card-text:nth-child(1)")).Text;
                var conditionOnWebPage = _webDriver.FindElement(By.CssSelector(".current-weather .card-text:nth-child(2)")).Text;
                var pressureOnWebPage = _webDriver.FindElement(By.CssSelector(".current-weather .card-text:nth-child(3)")).Text;
                var humidityOnWebPage = _webDriver.FindElement(By.CssSelector(".current-weather .card-text:nth-child(4)")).Text;
                var cloudCoverOnWebPage = _webDriver.FindElement(By.CssSelector(".weather-details-right .card-text:nth-child(1)")).Text;
                var feelsLikeOnWebPage = _webDriver.FindElement(By.CssSelector(".weather-details-right .card-text:nth-child(2)")).Text;
                var windSpeedOnWebPage = _webDriver.FindElement(By.CssSelector(".weather-details-right .card-text:nth-child(3)")).Text;

                Assert.AreEqual($"Temperature: {apiData.Current.TemperatureC} °C ({apiData.Current.TemperatureF} °F)", temperatureOnWebPage);
                Assert.AreEqual($"Condition: {(apiData.Current.Condition?.Text ?? "Not available")}", conditionOnWebPage);
                Assert.AreEqual($"Pressure: {apiData.Current.PressureIn} inches", pressureOnWebPage);
                Assert.AreEqual($"Humidity: {apiData.Current.Humidity} %", humidityOnWebPage);
                Assert.AreEqual($"Cloud Cover: {apiData.Current.Cloud} %", cloudCoverOnWebPage);
                Assert.AreEqual($"Feels Like:{apiData.Current.FeelsLikeC} °C", feelsLikeOnWebPage);
                Assert.AreEqual($"Wind Speed: {apiData.Current.WindKph} kph", windSpeedOnWebPage);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error making API request: {ex.Message}");
                throw;
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            _webDriver.Quit();
            _webDriver.Dispose();

        }
    }

}