using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Core.Services
{
    public class ChoreService : IChoreService
    {
        private readonly IChoreRepository _choreRepository;

        public ChoreService(IChoreRepository choreRepository)
        {
            _choreRepository = choreRepository;
        }

        public async Task<IEnumerable<Chore>> GetAllChores()
        {
            return await _choreRepository.GetAllChores();
        }

        public async Task AddChore(Chore chore)
        {
            //Fetchoing all chores to check against
            var chores = await _choreRepository.GetAllChores();

            if (chores.Any(u => u.Name == chore.Name))
                throw new InvalidOperationException("Chore already exits");

            if (chore.RecurrenceType == RecurrenceType.Weekly && chore.DayOfWeek == null)
                throw new ArgumentException("DayOfWeek cannot be null ");
           
            await _choreRepository.AddChore(chore);
        }

        public async Task UpdateChore(int id, Chore chore)
        {
            var existingChore = await _choreRepository.GetChoreById(id);

            if (existingChore == null)
                throw new InvalidOperationException("Chore not found");

            var chores = await _choreRepository.GetAllChores();
            if (chores.Any(c => c.Name == chore.Name && c.Id != id))
                throw new InvalidOperationException("Chore name already exists");

            chore.Id = id;
            await _choreRepository.UpdateChore(chore);
        }

        public async Task DeleteChore(int id)
        {
            var existingChore = await _choreRepository.GetChoreById(id);

            if (existingChore == null)
                throw new InvalidOperationException("Chore not found");

           await _choreRepository.DeleteChore(id);

        }
    }
   
}
