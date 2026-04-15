using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Interfaces
{
    public interface IChoreService
    {
        Task<IEnumerable<Chore>> GetAllChores();
        Task AddChore(Chore chore);

        Task UpdateChore(int id, Chore chore);
        Task DeleteChore(int id);
    }
}
