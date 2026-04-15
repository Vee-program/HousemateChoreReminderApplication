using HousemateChoreReminderAPI.Chores.Core.Models;

namespace HousemateChoreReminderAPI.Chores.API.DTOs.Assignment
{
    public class AssignmentResponse
    {
        public int Id { get; set; }

        public string HousemateName { get; set; }

        public string ChoreName { get; set; }

        public DateTime DueDate { get; set; }

        public AssignmentStatus Status { get; set; }
    }
}
