namespace HousemateChoreReminderAPI.Chores.API.DTOs.Assignment
{
    public class AssignmentCreateDTO
    {
       public int ChoreId { get; set; }
        public int HousemateId { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now;
    }
}
