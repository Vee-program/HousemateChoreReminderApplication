using Chores.Core.Interfaces;
using Chores.Core.Services;
using HousemateChoreReminderAPI.Chores.API.DTOs.Housemate;
using HousemateChoreReminderAPI.Chores.API.DTOs.Reminder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HousemateChoreReminderAPI.Chores.API.Controllers
{

    [Authorize]

    [ApiController]
    [Route("api/[controller]")]
    public class ReminderController: ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllReminders()
        {
            var reminders = await _reminderService.GetAllReminders();

            var response = reminders.Select(c => new ReminderResponseDTO
            {
               ChoreName = c.Assignment.Chore.Name,
               dueDate = c.Assignment.DueDate,
                IsSent = c.IsSent,
            });
            return Ok(response);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SendingPendingReminders() {
        
            await _reminderService.SendingPendingReminders();
            return Ok();
        }
        
            
    }
}
