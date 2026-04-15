using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.Core.Models;
using HousemateChoreReminderAPI.Chores.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chores.Infrastructure.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;

        public ReminderRepository(AppDbContext context)
        {
            _context = context;
        }

        //rotation logic creates reminders alongside new assignments
        public async Task AddReminder(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
        }

        // admin views all reminders
        public async Task<IEnumerable<Reminder>> GetAllReminders()
        {
            return await _context.Reminders 
                .Include(r => r.Assignment)
                    .ThenInclude(a => a.Housemate)
                .Include(r => r.Assignment)
                    .ThenInclude(a => a.Chore)
                .ToListAsync();
        }

        // background job fetches reminders where IsSent is false
        public async Task<IEnumerable<Reminder>> GetUnsentReminders()
        {
            return await _context.Reminders
                .Where(r => !r.IsSent && r.ScheduledSendTime <= DateTime.UtcNow)
                .Include(r => r.Assignment)
                    .ThenInclude(a => a.Housemate)
                .Include(r => r.Assignment)
                    .ThenInclude(a => a.Chore)
                .ToListAsync();
        }

        //after WhatsApp message is sent, marks reminder as sent
        public async Task UpdateReminderSentStatus(int reminderId)
        {
            var reminder = await _context.Reminders.FindAsync(reminderId);

            if (reminder != null)
            {
                reminder.IsSent = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
