using Newtonsoft.Json;
using ForecastFavorLib.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ForecastFavorApp.Data;


namespace ForecastFavorApp_UnitTest
{
    // test class for the CurrentWeatherResponseTests
    [TestClass]
    public class ForecastFavorAppTest
    {
        [TestMethod]
        public void CurrentWeatherResponseTests()
        {
            string json = @"{
                'location': {
                    'name': 'CityName',
                    'region': 'RegionName',
                    'country': 'CountryName',
                    'lat': 40.7128,
                    'lon': -74.0060,
                    'tz_id': 'TimeZoneId',
                    'localtime': '2023-12-01 12:00:00'
                },
                'current': {
                    'last_updated': '2023-12-01 12:00:00',
                    'temp_c': 25.5,
                    'temp_f': 77.9,
                    'condition': {
                        'text': 'Clear',
                        'icon': 'https://example.com/clear.png',
                        'code': 800
                    },
                    'pressure_in': 29.92,
                    'humidity': 50,
                    'cloud': 20,
                    'feelslike_c': 26.0,
                    'wind_kph': 10.0
                }
            }";

            try
            {
                CurrentWeatherResponse response = JsonConvert.DeserializeObject<CurrentWeatherResponse>(json);
                Assert.IsNotNull(response);
                Assert.IsNotNull(response.Location);
                Assert.IsNotNull(response.Current);
                Assert.AreEqual("CityName", response.Location?.Name);
                Assert.AreEqual("RegionName", response.Location?.Region);
                Assert.AreEqual("CountryName", response.Location?.Country);
                Assert.AreEqual(40.7128, response.Location?.Latitude);
                Assert.AreEqual(-74.0060, response.Location?.Longitude);
                Assert.AreEqual("TimeZoneId", response.Location?.TimeZoneId);
                Assert.AreEqual("2023-12-01 12:00:00", response.Location?.LocalTime);
                Assert.AreEqual("2023-12-01 12:00:00", response.Current.LastUpdated);
                Assert.AreEqual(25.5, response.Current.TemperatureC);
                Assert.AreEqual(77.9, response.Current.TemperatureF);
                Assert.IsNotNull(response.Current.Condition);
                Assert.AreEqual("Clear", response.Current.Condition.Text);
                Assert.AreEqual("https://example.com/clear.png", response.Current.Condition.Icon);
                Assert.AreEqual(800, response.Current.Condition.Code);
                Assert.AreEqual(29.92, response.Current.PressureIn);
                Assert.AreEqual(50, response.Current.Humidity);
                Assert.AreEqual(20, response.Current.Cloud);
                Assert.AreEqual(26.0, response.Current.FeelsLikeC);
                Assert.AreEqual(10.0, response.Current.WindKph);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                throw;
            }
        }
    }

    // test class for the ConditionTest
    [TestClass]
    public class ConditionTest
    {
        [TestMethod]
        public void ConditionSerializationTest()
        {
            Condition condition = new Condition
            {
                Text = "Clear",
                Icon = "https://example.com/clear.png",
                Code = 800
            };
            string json = JsonConvert.SerializeObject(condition);
            Assert.IsNotNull(json);
            Condition deserializedCondition = JsonConvert.DeserializeObject<Condition>(json);
            Assert.IsNotNull(deserializedCondition);
            Assert.AreEqual("Clear", deserializedCondition.Text);
            Assert.AreEqual("https://example.com/clear.png", deserializedCondition.Icon);
            Assert.AreEqual(800, deserializedCondition.Code);
        }
    }

    // test class for the DayTest
    [TestClass]
    public class DayTest
    {
        [TestMethod]
        public void DaySerializationTest()
        {
            Day day = new Day
            {
                MaxTempC = 28.5,
                MinTempC = 18.5,
                Condition = new Condition
                {
                    Text = "Partly cloudy",
                    Icon = "https://example.com/partly-cloudy.png",
                    Code = 801
                },
                AvgTempC = 23.5,
                MaxWindKph = 15.0,
                TotalPrecipIn = 0.5,
                TotalSnowCm = 2.0,
                AvgHumidity = 60.0,
                DailyChanceOfRain = 30,
                DailyChanceOfSnow = 10
            };
            string json = JsonConvert.SerializeObject(day);
            Assert.IsNotNull(json);
            Day deserializedDay = JsonConvert.DeserializeObject<Day>(json);
            Assert.IsNotNull(deserializedDay);
            Assert.AreEqual(28.5, deserializedDay.MaxTempC);
            Assert.AreEqual(18.5, deserializedDay.MinTempC);
            Assert.IsNotNull(deserializedDay.Condition);
            Assert.AreEqual("Partly cloudy", deserializedDay.Condition.Text);
            Assert.AreEqual("https://example.com/partly-cloudy.png", deserializedDay.Condition.Icon);
            Assert.AreEqual(801, deserializedDay.Condition.Code);
            Assert.AreEqual(23.5, deserializedDay.AvgTempC);
            Assert.AreEqual(15.0, deserializedDay.MaxWindKph);
            Assert.AreEqual(0.5, deserializedDay.TotalPrecipIn);
            Assert.AreEqual(2.0, deserializedDay.TotalSnowCm);
            Assert.AreEqual(60.0, deserializedDay.AvgHumidity);
            Assert.AreEqual(30, deserializedDay.DailyChanceOfRain);
            Assert.AreEqual(10, deserializedDay.DailyChanceOfSnow);
        }
    }

    // test class for the ForecastDayTest
    [TestClass]
    public class ForecastDayTest
    {
        [TestMethod]
        public void ForecastDaySerializationTest()
        {
            ForecastDay forecastDay = new ForecastDay
            {
                Date = "2023-12-01",
                Day = new Day
                {
                    MaxTempC = 28.5,
                    MinTempC = 18.5,
                    Condition = new Condition
                    {
                        Text = "Partly cloudy",
                        Icon = "https://example.com/partly-cloudy.png",
                        Code = 801
                    },
                    AvgTempC = 23.5,
                    MaxWindKph = 15.0,
                    TotalPrecipIn = 0.5,
                    TotalSnowCm = 2.0,
                    AvgHumidity = 60.0,
                    DailyChanceOfRain = 30,
                    DailyChanceOfSnow = 10
                },
                HourlyForecasts = new List<Hour>
                {
                    new Hour
                    {
                        Time = "12:00",
                        TempC = 25.0,
                        TempF = 77.0,
                        Condition = new Condition
                        {
                            Text = "Clear",
                            Icon = "https://example.com/clear.png",
                            Code = 800
                        },
                        WindKph = 10.0
                    },
                }
            };
            string json = JsonConvert.SerializeObject(forecastDay);
            Assert.IsNotNull(json);
            ForecastDay deserializedForecastDay = JsonConvert.DeserializeObject<ForecastDay>(json);
            Assert.IsNotNull(deserializedForecastDay);
            Assert.AreEqual("2023-12-01", deserializedForecastDay.Date);
            Assert.IsNotNull(deserializedForecastDay.Day);
            Assert.AreEqual(28.5, deserializedForecastDay.Day.MaxTempC);
            Assert.AreEqual(18.5, deserializedForecastDay.Day.MinTempC);
            Assert.IsNotNull(deserializedForecastDay.Day.Condition);
            Assert.AreEqual("Partly cloudy", deserializedForecastDay.Day.Condition.Text);
            Assert.AreEqual("https://example.com/partly-cloudy.png", deserializedForecastDay.Day.Condition.Icon);
            Assert.AreEqual(801, deserializedForecastDay.Day.Condition.Code);
            Assert.AreEqual(23.5, deserializedForecastDay.Day.AvgTempC);
            Assert.AreEqual(15.0, deserializedForecastDay.Day.MaxWindKph);
            Assert.AreEqual(0.5, deserializedForecastDay.Day.TotalPrecipIn);
            Assert.AreEqual(2.0, deserializedForecastDay.Day.TotalSnowCm);
            Assert.AreEqual(60.0, deserializedForecastDay.Day.AvgHumidity);
            Assert.AreEqual(30, deserializedForecastDay.Day.DailyChanceOfRain);
            Assert.AreEqual(10, deserializedForecastDay.Day.DailyChanceOfSnow);
            Assert.IsNotNull(deserializedForecastDay.HourlyForecasts);
            Assert.AreEqual(1, deserializedForecastDay.HourlyForecasts.Count);
            Hour deserializedHour = deserializedForecastDay.HourlyForecasts[0];
            Assert.IsNotNull(deserializedHour);
            Assert.AreEqual("12:00", deserializedHour.Time);
            Assert.AreEqual(25.0, deserializedHour.TempC);
            Assert.AreEqual(77.0, deserializedHour.TempF);
            Assert.IsNotNull(deserializedHour.Condition);
            Assert.AreEqual("Clear", deserializedHour.Condition.Text);
            Assert.AreEqual("https://example.com/clear.png", deserializedHour.Condition.Icon);
            Assert.AreEqual(800, deserializedHour.Condition.Code);
            Assert.AreEqual(10.0, deserializedHour.WindKph);
        }
    }
    // Test Class for ForcastResponse
    [TestClass]
    public class ForecastResponseTest
    {
        [TestMethod]
        public void ForecastResponseSerializationTest()
        {
            ForecastResponse forecastResponse = new ForecastResponse
            {
                Location = new Location
                {
                    Name = "City",
                    Region = "Region",
                    Country = "Country",
                    Latitude = 40.7128,
                    Longitude = -74.0060,
                    TimeZoneId = "America/New_York",
                    LocalTime = "2023-12-01 12:00:00"
                },
                Current = new CurrentWeatherResponse
                {
                    Location = new Location
                    {
                        Name = "City",
                        Region = "Region",
                        Country = "Country",
                        Latitude = 40.7128,
                        Longitude = -74.0060,
                        TimeZoneId = "America/New_York",
                        LocalTime = "2023-12-01 12:00:00"
                    },
                    Current = new Current
                    {
                        LastUpdated = "2023-12-01 12:00:00",
                        TemperatureC = 25.5,
                        TemperatureF = 77.9,
                        Condition = new Condition
                        {
                            Text = "Clear",
                            Icon = "https://example.com/clear.png",
                            Code = 800
                        },
                        PressureIn = 29.92,
                        Humidity = 50,
                        Cloud = 20,
                        FeelsLikeC = 26.0,
                        WindKph = 10.0
                    }
                },
                Forecast = new Forecast
                {
                    ForecastDay = new List<ForecastDay>
                    {
                        new ForecastDay
                        {
                            Date = "2023-12-01",
                            Day = new Day
                            {
                                MaxTempC = 28.5,
                                MinTempC = 18.5,
                                Condition = new Condition
                                {
                                    Text = "Partly cloudy",
                                    Icon = "https://example.com/partly-cloudy.png",
                                    Code = 801
                                },
                                AvgTempC = 23.5,
                                MaxWindKph = 15.0,
                                TotalPrecipIn = 0.5,
                                TotalSnowCm = 2.0,
                                AvgHumidity = 60.0,
                                DailyChanceOfRain = 30,
                                DailyChanceOfSnow = 10
                            },
                            HourlyForecasts = new List<Hour>
                            {
                                new Hour
                                {
                                    Time = "12:00",
                                    TempC = 25.0,
                                    TempF = 77.0,
                                    Condition = new Condition
                                    {
                                        Text = "Clear",
                                        Icon = "https://example.com/clear.png",
                                        Code = 800
                                    },
                                    WindKph = 10.0
                                }
                            }
                        }
                    }
                }
            };
            string json = JsonConvert.SerializeObject(forecastResponse);
            ForecastResponse deserializedForecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(json);
            Assert.AreEqual("City", deserializedForecastResponse.Location.Name);
            Assert.IsNotNull(deserializedForecastResponse.Current);
            Assert.IsNotNull(deserializedForecastResponse.Forecast);
            Assert.IsNotNull(deserializedForecastResponse.Forecast.ForecastDay);
            Assert.AreEqual(1, deserializedForecastResponse.Forecast.ForecastDay.Count);
        }
    }
    // test class for Hour
    [TestClass]
    public class HourTest
    {
        [TestMethod]
        public void HourSerializationTest()
        {
            Hour hour = new Hour
            {
                TimeEpoch = 1638379200,
                Time = "2023-12-01 12:00:00",
                TempC = 25.0,
                TempF = 77.0,
                Condition = new Condition
                {
                    Text = "Clear",
                    Icon = "https://example.com/clear.png",
                    Code = 800
                },
                WindMph = 5.0,
                WindKph = 8.0,
                WindDegree = 180,
                WindDir = "S",
                PressureIn = 29.92,
                PrecipIn = 0.0,
                Humidity = 50,
                Cloud = 10,
                FeelsLikeC = 26.0,
                WindChillC = 24.0,
                HeatIndexC = 27.0,
                DewPointC = 15.0,
                WillItRain = 0,
                ChanceOfRain = 0,
                WillItSnow = 0,
                ChanceOfSnow = 0,
                Uv = 5.0
            };
            string json = JsonConvert.SerializeObject(hour);
            Assert.IsNotNull(json);
            Hour deserializedHour = JsonConvert.DeserializeObject<Hour>(json);
            Assert.IsNotNull(deserializedHour);
            Assert.AreEqual("2023-12-01 12:00:00", deserializedHour.Time);
            Assert.IsNotNull(deserializedHour.Condition);
            Assert.AreEqual("Clear", deserializedHour.Condition.Text);
            Assert.AreEqual("https://example.com/clear.png", deserializedHour.Condition.Icon);
            Assert.AreEqual(800, deserializedHour.Condition.Code);
        }
    }
    // test class for the location
    [TestClass]
    public class LocationTest
    {
        [TestMethod]
        public void LocationSerializationTest()
        {
            Location location = new Location
            {
                Name = "City",
                Region = "Region",
                Country = "Country",
                Latitude = 40.7128,
                Longitude = -74.0060,
                TimeZoneId = "America/New_York",
                LocalTime = "2023-12-01 12:00:00"
            };
            string json = JsonConvert.SerializeObject(location);
            Assert.IsNotNull(json);
            Location deserializedLocation = JsonConvert.DeserializeObject<Location>(json);
            Assert.IsNotNull(deserializedLocation);
            Assert.AreEqual("City", deserializedLocation.Name);
            Assert.AreEqual("America/New_York", deserializedLocation.TimeZoneId);
        }
    }
    // test class for the CalendarEvent
    [TestClass]
    public class CalendarEventTests
    {
        [TestMethod]
        public void CalendarEvent_Validation_Success()
        {
            var calendarEvent = new CalendarEvent
            {
                EventID = 1,
                UserID = 123,
                EventTitle = "Sample Event",
                EventDate = DateTime.Now.Date,
                EventTime = TimeSpan.FromHours(12),
                EventLocation = "Sample Location",
                Notes = "Sample Notes",
                User = new User { }
            };
            var validationContext = new ValidationContext(calendarEvent, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(calendarEvent, validationContext, validationResults, true);

            Assert.IsTrue(isValid, "Validation should pass for a valid CalendarEvent");
        }

    }
    // test class for the Preferences
    [TestClass]
    public class PreferencesTests
    {
        [TestMethod]
        public void Preferences_Validation_Success()
        {
            var preferences = new Preferences
            {
                PreferencesID = 1,
                UserID = 123,
                NotifyOnRain = true,
                NotifyOnSun = false,
                NotifyOnClouds = true,
                NotifyOnSnow = false,
                PreferredLocations = "City1, City2",
                User = new User { }
            };
            var validationContext = new ValidationContext(preferences, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(preferences, validationContext, validationResults, true);

            Assert.IsTrue(isValid, "Validation should pass for valid Preferences");
        }
    }
    // test class for the User
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_Validation_Success()
        {
            var user = new User
            {
                UserID = 1,
                Username = "Sreenath",
                Email = "sreenath.segar@example.com",
                Preferences = new Preferences { },
                CalendarEvents = new List<CalendarEvent> { },
                WeatherHistories = new List<WeatherHistory> { }
            };
            var validationContext = new ValidationContext(user, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

            Assert.IsTrue(isValid, "Validation should pass for a valid User");
        }
    }
    // test class for the WeatherHistory 
    [TestClass]
    public class WeatherHistoryTests
    {
        [TestMethod]
        public void WeatherHistory_Validation_Success()
        {
            var weatherHistory = new WeatherHistory
            {
                HistoryID = 1,
                UserID = 123,
                Location = "Sample Location",
                Date = DateTime.Now.Date,
                Temperature = 25.5,
                Precipitation = 15.0,
                User = new User { }
            };
            var validationContext = new ValidationContext(weatherHistory, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(weatherHistory, validationContext, validationResults, true);

            Assert.IsTrue(isValid, "Validation should pass for valid WeatherHistory");
        }
    }
    // test class for the AppDbContext
    [TestClass]
    public class AppDbContextTests
    {
        [TestMethod]
        public void AppDbContext_CanSaveAndRetrieveEntities()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var user = new User
                {
                    UserID = 1,
                    Username = "Sreenath",
                    Email = "sreenath.segar@example.com",
                    Preferences = new Preferences
                    {
                        NotifyOnRain = true,
                        NotifyOnSun = false,
                        NotifyOnClouds = true,
                        NotifyOnSnow = false,
                        PreferredLocations = "Some locations"
                    },
                    CalendarEvents = new List<CalendarEvent>(),
                    WeatherHistories = new List<WeatherHistory>()
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                var savedUser = context.Users.Include(u => u.Preferences).FirstOrDefault(u => u.UserID == 1);
                Assert.IsNotNull(savedUser);
                Assert.AreEqual("Sreenath", savedUser.Username);
                Assert.IsNotNull(savedUser.Preferences);
                Assert.IsTrue(savedUser.Preferences.NotifyOnRain);
                Assert.IsFalse(savedUser.Preferences.NotifyOnSun);
                Assert.IsTrue(savedUser.Preferences.NotifyOnClouds);
                Assert.IsFalse(savedUser.Preferences.NotifyOnSnow);
                Assert.AreEqual("Some locations", savedUser.Preferences.PreferredLocations);
            }
        }
    }
}
