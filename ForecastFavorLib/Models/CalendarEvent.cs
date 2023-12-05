using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForecastFavorLib.Models
{
    public class CalendarEvent
    {
    [Key]
    public int EventID { get; set; }

    [ForeignKey("User")]
    public int UserID { get; set; }

    [Required]
    [StringLength(200)]
    public string EventTitle { get; set; }

    [Required]
    public DateTime EventDate { get; set; }

    [Required]
    public TimeSpan EventTime { get; set; }

    [StringLength(200)]
    public string EventLocation { get; set; }

    [StringLength(1000)]
    public string Notes { get; set; }

    // Navigation property
    public User User { get; set; }
    }
  

}
