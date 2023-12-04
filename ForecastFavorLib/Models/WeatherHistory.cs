using System;
using System.ComponentModel.DataAnnotations;

namespace ForecastFavorLib.Models
{
    public class WeatherHistory
    {
    [Key]
    public int HistoryID { get; set; }
    public int UserID { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public double Temperature { get; set; }
    public double Precipitation { get; set; }
    
    // Navigation property
    public User User { get; set; }
    }
}
