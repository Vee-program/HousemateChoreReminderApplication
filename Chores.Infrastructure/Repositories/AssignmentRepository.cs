using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.Core.Models;
using HousemateChoreReminderAPI.Chores.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Chores.Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _context;

        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        //rotation logic creates new assignments
        public async Task AddAssignment(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }

        //admin views all assignments
        public async Task<IEnumerable<Assignment>> GetAllAssignments()
        {
            return await _context.Assignments
                .Include(a => a.Housemate)
                .Include(a => a.Chore)
                .ToListAsync();
        }

        //housemate views their own assignments
        public async Task<IEnumerable<Assignment>> GetAssignmentsByHousemate(int housemateId)
        {
            return await _context.Assignments
                .Where(a => a.HousemateId == housemateId)
                .Include(a => a.Chore)
                .ToListAsync();
        }

        //filter by week for admin
        public async Task<IEnumerable<Assignment>> GetAssignmentsByWeek(DateTime weekStartDate)
        {
            return await _context.Assignments
                .Where(a => a.WeekStartDate == weekStartDate)
                .Include(a => a.Housemate)
                .Include(a => a.Chore)
                .ToListAsync();
        }
        //background job fetches assignments where due date has passed and status isn't Done
        public async Task<IEnumerable<Assignment>> GetOverdueAssignments()
        {
            return await _context.Assignments
                .Where(a => a.Status != AssignmentStatus.Done && a.DueDate < DateTime.UtcNow)
                .Include(a => a.Housemate)
                .Include(a => a.Chore)
                .ToListAsync();
        }

        //rotation logic needs to know previous assignments to determine who gets what next
        public async Task<IEnumerable<Assignment>> GetLastWeekAssignments()
        {
            var lastWeekStart = DateTime.UtcNow.AddDays(-7);

            return await _context.Assignments
                .Where(a => a.WeekStartDate == lastWeekStart)
                .Include(a => a.Housemate)
                .Include(a => a.Chore)
                .ToListAsync();
        }

        public async Task UpdateStatus(int assignmentId, AssignmentStatus newStatus)
        {
            var assignment = await _context.Assignments.FindAsync(assignmentId);

            if (assignment != null)
            {
                assignment.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
    }
