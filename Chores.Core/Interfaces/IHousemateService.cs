using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Interfaces
{
    public interface IHousemateService
    {
        Task<IEnumerable<Housemate>> GetAllHousemates();
        Task<Housemate> GetHousemateById(int id);
        Task AddHousemate(Housemate housemate);
        Task UpdateHousemate(int id, Housemate housemate);
        Task TransferAdmin(int targetHousemateId);
        Task DeleteHousemate(int id);
    }
}
