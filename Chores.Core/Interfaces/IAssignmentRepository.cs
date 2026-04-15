using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Interfaces
{
    public interface IAssignmentRepository
    {
        Task AddAssignment(Assignment assignment);
        Task<IEnumerable<Assignment>> GetAllAssignments();
        Task<IEnumerable<Assignment>> GetAssignmentsByHousemate(int housemateId);
        Task<IEnumerable<Assignment>> GetAssignmentsByWeek(DateTime weekStartDate);
        Task<IEnumerable<Assignment>> GetOverdueAssignments();
        Task<IEnumerable<Assignment>> GetLastWeekAssignments();
        Task UpdateStatus(int assignmentId, AssignmentStatus newStatus);
    }
}
