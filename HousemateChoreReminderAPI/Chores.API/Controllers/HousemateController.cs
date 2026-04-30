using Chores.Core.Interfaces;
using HousemateChoreReminderAPI.Chores.API.DTOs;
using HousemateChoreReminderAPI.Chores.API.DTOs.Housemate;
using HousemateChoreReminderAPI.Chores.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HousemateChoreReminderAPI.Chores.API.Controllers
{
    [Authorize]

    [ApiController]
    [Route("api/[controller]")]
    public class HousematesController : ControllerBase
    {
        private readonly IHousemateService _housemateService;

        public HousematesController(IHousemateService housemateService)
        {
            _housemateService = housemateService;
        }


        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> GetAllHousemates()
        {
            var housemates = await _housemateService.GetAllHousemates();

            var response = housemates.Select(c => new HousemateResponseAdminDTO
            {
                Id = c.Id,
                username = c.Username,
                phoneNumber = c.PhoneNumber
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHousemateById(int id)
        {
           
            try
            {
                var housemate = await _housemateService.GetHousemateById(id);

                var response = new HousemateResponseUserDTO
                {
                    Id = housemate.Id,
                    username = housemate.Username

                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHousemate(int id, [FromBody] HousemateUpdateDTO dto)
        {
            try
            {
                var housemate = new Housemate
                {
                    Username = dto.username,
                    PhoneNumber = dto.phoneNumber
                };

                await _housemateService.UpdateHousemate(id, housemate);

                return Ok("Housemate updated successfully");
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpPatch("{id}/make-admin")]
        public async Task<IActionResult> TransferAdmin(int id)
        {
            try
            {
                await _housemateService.TransferAdmin(id);
                return Ok("Admin transferred successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHousemate(int id)
        {


            try
            {
                await _housemateService.DeleteHousemate(id);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}