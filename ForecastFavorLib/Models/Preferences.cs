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

    [StringLength(500)]
    public string NotificationTriggers { get; set; }

    [StringLength(500)]
    public string PreferredLocations { get; set; }

    // Navigation property
    public User User { get; set; }
    }


}
