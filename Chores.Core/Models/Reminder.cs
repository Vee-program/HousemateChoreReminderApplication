using System.ComponentModel.DataAnnotations;

namespace HousemateChoreReminderAPI.Chores.Core.Models
{
    public class Reminder
    {
        [Key]
        public int Id { get; set; }

        public int AssignmentId { get; set; }
        public Assignment ? Assignment { get; set; }

        public DateTime ScheduledSendTime { get; set; }

        public bool IsSent { get; set; }
    }
}
