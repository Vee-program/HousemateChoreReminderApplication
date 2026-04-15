using HousemateChoreReminderAPI.Chores.Core.Models;

namespace HousemateChoreReminderAPI.Chores.API.DTOs.Chore
{
    public class ChoreUpdateDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }


        public RecurrenceType RecurrenceType { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }
    }
}
