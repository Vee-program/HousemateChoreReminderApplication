using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HousemateChoreReminderAPI.Chores.Core.Models
{
    public class Chore
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public DayOfWeek? DayOfWeek { get; set; } 
    }
}
