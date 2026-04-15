using HousemateChoreReminderAPI.Chores.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace HousemateChoreReminderAPI.Chores.API.DTOs.Chore
{
    public class ChoreCreateDTO
    {
        
        public string Name { get; set; }

        public string Description { get; set; }

       
        public RecurrenceType RecurrenceType { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }
    }
}
