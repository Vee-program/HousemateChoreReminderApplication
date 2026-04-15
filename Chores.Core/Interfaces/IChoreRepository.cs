using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Interfaces
{
    public interface IChoreRepository
    {
        Task<IEnumerable<Chore>> GetAllChores();
        Task<Chore?> GetChoreById(int id);
        Task AddChore(Chore chore);
        Task UpdateChore(Chore chore);
        Task DeleteChore(int id);
    }
}
