
using System.ComponentModel.DataAnnotations;

namespace HousemateChoreReminderAPI.Chores.Core.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        public int HousemateId { get; set; }
       
        public Housemate ? Housemate { get; set; }

        public int ChoreId { get; set; }
        public Chore ? Chore { get; set; }

        public DateTime DueDate { get; set; }

        public AssignmentStatus Status { get; set; }

        public DateTime WeekStartDate { get; set; }
    }
}
