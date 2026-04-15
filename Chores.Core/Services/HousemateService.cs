using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Services
{ 
        public class HousemateService : IHousemateService
        {
            private readonly IHousemateRepository _housemateRepository;

            public HousemateService(IHousemateRepository housemateRepository)
            {
                _housemateRepository = housemateRepository;
            }

            public async Task<IEnumerable<Housemate>> GetAllHousemates()
            {
                return await _housemateRepository.GetAllHousemates();
            }

        public async Task<Housemate> GetHousemateById(int id)
        {
            var housemate = await _housemateRepository.GetHousemateById(id);

            if (housemate == null)
                throw new InvalidOperationException("Housemate not found");

            return housemate;
        }
        public async Task AddHousemate(Housemate housemate)
            {
                var housemates = await _housemateRepository.GetAllHousemates();

                if (housemates.Any(h => h.Username == housemate.Username))
                    throw new InvalidOperationException("Housemate already exists");

                await _housemateRepository.AddHousemate(housemate);
            }

            public async Task UpdateHousemate(int id, Housemate housemate)
            {
                var existingHousemate = await _housemateRepository.GetHousemateById(id);

                if (existingHousemate == null)
                    throw new InvalidOperationException("Housemate not found");

                var housemates = await _housemateRepository.GetAllHousemates();

                if (housemates.Any(h => h.Username == housemate.Username && h.Id != id))
                    throw new InvalidOperationException("Housemate name already exists");

                housemate.Id = id;

                await _housemateRepository.UpdateHousemate(housemate);
            }
         
        public async Task TransferAdmin(int targetHousemateId)
        {
            var housemate = await _housemateRepository.GetHousemateById(targetHousemateId);

            if (housemate == null)
                throw new InvalidOperationException("Housemate not found");
            if (housemate.IsAdmin)
                throw new InvalidOperationException("The user is already admin");

            var admin = await _housemateRepository.GetAdmin();

            admin.IsAdmin = false;
            housemate.IsAdmin = true;

            await _housemateRepository.UpdateAdminStatus(admin.Id, false);
            await _housemateRepository.UpdateAdminStatus(housemate.Id, true);

        }
        public async Task DeleteHousemate(int id)
            {
                var existingHousemate = await _housemateRepository.GetHousemateById(id);

                if (existingHousemate == null)
                    throw new InvalidOperationException("Housemate not found");

                await _housemateRepository.DeleteHousemate(id);
            }
        }
    }

