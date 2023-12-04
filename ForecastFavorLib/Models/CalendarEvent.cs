using System;
using System.ComponentModel.DataAnnotations;

namespace ForecastFavorLib.Models
{
    public class CalendarEvent
    {
    [Key]
    public int EventID { get; set; }
    public int UserID { get; set; }
    public string EventTitle { get; set; }
    public DateTime EventDate { get; set; }
    public TimeSpan EventTime { get; set; }
    public string EventLocation { get; set; }
    public string Notes { get; set; }
    
    // Navigation property
    public User User { get; set; }
    }   

}
