using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForecastFavorLib.Models
{
   public class Preferences
    {
    [Key]
    public int PreferencesID { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }

    // Modify to use boolean flags for weather conditions
    public bool NotifyOnRain { get; set; }
    public bool NotifyOnSun { get; set; }
    public bool NotifyOnClouds { get; set; }
    public bool NotifyOnSnow { get; set; }
    
    [StringLength(500)]
    public string PreferredLocations { get; set; }

    // Navigation property
    public User User { get; set; }
    }


}
