using DTO.classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService UserService;
        ILogger<string> logger;
        public UserController(IUserService service, ILogger<string> logger)
        {
            UserService = service;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await UserService.GetAllUsersAsync();
                return Ok(users); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to get all users: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await UserService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found"); // HTTP 404 Not Found
                }
                return Ok(user); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get user with ID {id}: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> GetByEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await UserService.GetByEmailAndByPasswordAsync(email, password);
                if (user == null)
                {
                    return NotFound("User not found with provided email and password"); // HTTP 404 Not Found
                }
                return Ok(user); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get user with email {email}: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserDTO newUser)
        {
            try
            {
                if (newUser == null)
                {
                    return BadRequest("User cannot be null"); // HTTP 400 Bad Request
                }

                await UserService.AddNewUserAsync(newUser);
                return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser); // HTTP 201 Created
            }
            catch (ArgumentException ex)
            {
                logger.LogError("Invalid argument: " + ex.Message);
                return BadRequest(ex.Message); // HTTP 400 Bad Request
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to add user: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User cannot be null"); // HTTP 400 Bad Request
                }

                var updatedUser = await UserService.UpdateAsync(user);
                return Ok(updatedUser); // HTTP 200 OK
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError("User not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to update user: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await UserService.DeleteAsync(id);
                return NoContent(); // HTTP 204 No Content
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("User not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to delete user: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }
    }
}
