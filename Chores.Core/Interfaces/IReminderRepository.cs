using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Interfaces
{
    public interface IReminderRepository
    {
        Task AddReminder(Reminder reminder);
        Task<IEnumerable<Reminder>> GetAllReminders();
        Task<IEnumerable<Reminder>> GetUnsentReminders();
        Task UpdateReminderSentStatus(int reminderId);
    }
}
