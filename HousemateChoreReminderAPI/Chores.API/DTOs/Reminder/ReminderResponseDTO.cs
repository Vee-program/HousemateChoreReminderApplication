namespace HousemateChoreReminderAPI.Chores.API.DTOs.Reminder
{
    public class ReminderResponseDTO
    {

        public string ChoreName { get; set; }
        public DateTime dueDate { get; set; }

        public DateTime ScheduledSendTime { get; set; }

        public bool IsSent { get; set; }
    }
}
