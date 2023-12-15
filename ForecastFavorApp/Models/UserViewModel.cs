using System.ComponentModel.DataAnnotations;

namespace ForecastFavorApp.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class AddEditUserViewModel
    {
        public int? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}