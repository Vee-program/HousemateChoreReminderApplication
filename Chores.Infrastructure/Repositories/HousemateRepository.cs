using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.Core.Models;
using HousemateChoreReminderAPI.Chores.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Infrastructure.Repositories
{
    /*The repository just fetches or saves data, the service applies the rules around that data.
     * Everything returns a Task or Task<T> — this is because all database operations should be asynchronous
     * AppDbContext is injected through the constructor — the repository never creates it manually
     * The implementation class says : IHousemateRepository meaning it promises to implement all methods defined in the interface */

    public class HousemateRepository : IHousemateRepository
    {
        private readonly AppDbContext _context;

        public HousemateRepository(AppDbContext context)
        {
            _context = context;
        }

        // needed for rotation logic to get all housemates
        public async Task<IEnumerable<Housemate>> GetAllHousemates()
        {
            return await _context.Housemates.ToListAsync();
        }

        public async Task<Housemate?> GetHousemateById(int id)
        {
            return await _context.Housemates.FindAsync(id);
        }

        public async Task<Housemate?> GetAdmin()
        {
            return await _context.Housemates.FirstOrDefaultAsync(u => u.IsAdmin);
        }

        //needed for login logic
        public async Task<Housemate?> GetHousemateByUsername(string username)
        {
            return await _context.Housemates
                .FirstOrDefaultAsync(h => h.Username == username);
        }

        //Admin creates a housemate
        public async Task AddHousemate(Housemate housemate)
        {
            await _context.Housemates.AddAsync(housemate);
            await _context.SaveChangesAsync();
        }

        //admin updates a housemate
        public async Task UpdateHousemate(Housemate housemate)
        {
            _context.Housemates.Update(housemate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdminStatus(int housemateId, bool isAdmin)
        {
            var housemate = await _context.Housemates.FindAsync(housemateId);

            if (housemate == null)
                throw new KeyNotFoundException("Housemate not found");

            housemate.IsAdmin = isAdmin;

            await _context.SaveChangesAsync();
        }

        public async Task<Housemate?> GetHousemateByPhoneNumber(string phoneNumber)
        {
            return await _context.Housemates
                .FirstOrDefaultAsync(h => h.PhoneNumber == phoneNumber);
        }
        //admin removes a housemate
        public async Task DeleteHousemate(int id)
        {
            var housemate = await _context.Housemates.FindAsync(id);

            if (housemate != null)
            {
                _context.Housemates.Remove(housemate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
