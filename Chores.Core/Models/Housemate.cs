using System.ComponentModel.DataAnnotations;

namespace HousemateChoreReminderAPI.Chores.Core.Models
{
    public class Housemate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        public string? PasswordHash { get; set; } // nullable

        public bool IsAdmin { get; set; }

        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
    }
}
