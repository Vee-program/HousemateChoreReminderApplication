using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.Core.Models;
using HousemateChoreReminderAPI.Chores.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Infrastructure.Repositories
{

    public class ChoreRepository : IChoreRepository
    {
        private readonly AppDbContext _context;

        public ChoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chore>> GetAllChores()
        {
            return await _context.Chores.ToListAsync();
        }

        public async Task<Chore?> GetChoreById(int id)
        {
            return await _context.Chores.FindAsync(id);
        }

        public async Task AddChore(Chore chore)
        {
            await _context.Chores.AddAsync(chore);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChore(Chore chore)
        {
            _context.Chores.Update(chore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChore(int id)
        {
            var chore = await _context.Chores.FindAsync(id);

            if (chore != null)
            {
                _context.Chores.Remove(chore);
                await _context.SaveChangesAsync();
            }
        }
    }
}
