using System;
using System.ComponentModel.DataAnnotations;

namespace ForecastFavorLib.Models
{
   public class Preferences
    {
        [Key]
        public int PreferencesID { get; set; }
        public int UserID { get; set; }
        public string NotificationTriggers { get; set; }
        public string PreferredLocations { get; set; }
        
        // Navigation property
        public User User { get; set; }
    }

}
