using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForecastFavorLib.Models
{
    public class WeatherHistory
{
    [Key]
    public int HistoryID { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }

    [Required]
    [StringLength(200)]
    public string Location { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Range(-100, 100)]
    public double Temperature { get; set; }

    [Range(0, 1000)]
    public double Precipitation { get; set; }

    // Navigation property
    public User User { get; set; }
}

}
