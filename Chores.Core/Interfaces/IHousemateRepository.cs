using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Interfaces
{
    public interface IHousemateRepository
    {
        Task<Housemate?> GetHousemateByUsername(string username);
        Task<IEnumerable<Housemate>> GetAllHousemates();
        Task<Housemate?> GetHousemateById(int id);
        Task<Housemate?> GetHousemateByPhoneNumber(string phoneNumber);
        Task<Housemate?> GetAdmin();
        Task AddHousemate(Housemate housemate);
        Task UpdateHousemate(Housemate housemate);
        Task UpdateAdminStatus(int housemateId, bool isAdmin);
        Task DeleteHousemate(int id);
    }
}
