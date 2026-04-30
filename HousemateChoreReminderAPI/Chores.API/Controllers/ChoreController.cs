using Azure;
using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.API.DTOs.Chore;
using HousemateChoreReminderAPI.Chores.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HousemateChoreReminderAPI.Chores.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChoreController : ControllerBase
    {
        private readonly IChoreService _choreService;

        public ChoreController(IChoreService choreService)
        {
            _choreService = choreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChores()
        {
            var chores = await _choreService.GetAllChores();

            var response = chores.Select(c => new ChoreResponseDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                RecurrenceType = c.RecurrenceType,
                DayOfWeek = c.DayOfWeek
            });

            return Ok(response);
        }


        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> AddChore([FromBody] ChoreCreateDTO dto)
        {
            try
            {
                var chore = new Chore
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    RecurrenceType = dto.RecurrenceType,
                    DayOfWeek = dto.DayOfWeek
                };

                await _choreService.AddChore(chore);

                return Ok("Chore created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChore(int id, [FromBody] ChoreUpdateDTO dto)
        {
            try
            {
                var chore = new Chore
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    RecurrenceType = dto.RecurrenceType,
                    DayOfWeek = dto.DayOfWeek
                };

                await _choreService.UpdateChore(id, chore);

                return Ok("Chore updated successfully");
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteChore(int id) {


            try
            {
                await _choreService.DeleteChore(id);

                return Ok();

            }
            catch(Exception ex) { 
            return BadRequest(ex.Message);
            }
            }
        }
    }
