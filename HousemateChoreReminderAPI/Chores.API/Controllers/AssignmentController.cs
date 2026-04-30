using HousemateChoreReminderAPI.Chores.API.DTOs.Assignment;
using HousemateChoreReminderAPI.Chores.Core.Models;
using Chores.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HousemateChoreReminderAPI.Chores.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAssignments()
        {
            var assignments = await _assignmentService.GetAllAssignments();

            var response = assignments.Select(a => new AssignmentResponseAdminDTO
            {
                Id = a.Id,
                ChoreName = a.Chore.Name,
                HousemateName = a.Housemate.Username,
                DueDate = a.DueDate,
                Status = a.Status
            });
            return Ok(response);
        }

        [HttpGet("housemate/{housemateId}")]

        public async Task<IActionResult> GetAssignmentsByHousemate(int housemateId)
        {
            var assignments = await _assignmentService.GetAssignmentsByHousemate(housemateId);
            var response = assignments.Select(a => new AssignmentResponseUserDTO
            {
                Id = a.Id,
                ChoreName = a.Chore.Name,
                DueDate = a.DueDate,
                Status = a.Status
            });
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateAssignments()
        {
            try
            {
                await _assignmentService.GenerateAssignments();
                return Ok("Assignments generated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAssignmentStatus(int id, [FromBody] AssignmentStatusUpdateDTO dto)
        {
            try
            {
                var isAdmin = User.IsInRole("Admin");
                var callerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _assignmentService.UpdateStatus(id, dto.Status, isAdmin, callerId);
                return Ok("Assignment status updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}