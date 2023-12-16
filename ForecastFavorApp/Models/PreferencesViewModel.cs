using System;
using System.ComponentModel.DataAnnotations.Schema;
using ForecastFavorLib.Models;

namespace ForecastFavorApp.Models
{
	public class PreferencesViewModel // : UserViewModel
	{
        public int PreferencesId { get; set; }

        public int UserId { get; set; }

        // Modify to use boolean flags for weather conditions
        public bool NotifyOnRain { get; set; }
        public bool NotifyOnSun { get; set; }
        public bool NotifyOnClouds { get; set; }
        public bool NotifyOnSnow { get; set; }
        public string PreferredLocations { get; set; }
        public User   User { get; set; }
    }
}

