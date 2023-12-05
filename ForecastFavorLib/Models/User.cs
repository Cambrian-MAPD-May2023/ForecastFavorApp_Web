using System;
using System.ComponentModel.DataAnnotations;

namespace ForecastFavorLib.Models
{
   public class User
{
    [Key]
    public int UserID { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // Navigation properties
    public Preferences Preferences { get; set; }
    public ICollection<CalendarEvent> CalendarEvents { get; set; }
    public ICollection<WeatherHistory> WeatherHistories { get; set; }
}

}
