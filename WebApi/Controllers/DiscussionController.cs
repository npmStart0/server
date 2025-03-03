using DTO.classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BLL.Interfaces;
using BLL.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        readonly IDiscussionService DiscussionService;
        readonly UserValidations userValidations;
        readonly ISubjectService subService;
        ILogger<string> logger;

        public DiscussionController(IDiscussionService service, UserValidations usValidate, ISubjectService sService, ILogger<string> logger)
        {
            DiscussionService = service;
            userValidations = usValidate;
            subService = sService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var discussions = await DiscussionService.GetAllDiscussionsAsync();
                return Ok(discussions); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to get all discussions: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var discussion = await DiscussionService.GetByIdAsync(id);
                if (discussion == null)
                {
                    return NotFound($"Discussion with ID {id} not found"); // HTTP 404
                }
                return Ok(discussion); // HTTP 200 OK
            }
            catch (ArgumentException ex)
            {
                logger.LogError($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message); // HTTP 400
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get discussion with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error"); // HTTP 500
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDiscussionDTO newDiscussion)
        {
            try
            {
                if (newDiscussion == null)
                {
                    return BadRequest("Discussion cannot be null"); // HTTP 400
                }

                var userExists = await userValidations.UserExistsAsync(newDiscussion.UserID);
                if (!userExists)
                {
                    return BadRequest("User does not exist."); // HTTP 400 Bad Request
                }

                var subExists = await subService.GetByIdAsync(newDiscussion.SubjectId);
                if (subExists == null) // שינויים כאן
                {
                    return BadRequest("Subject does not exist."); // HTTP 400 Bad Request
                }

                await DiscussionService.AddNewDiscussionAsync(newDiscussion);
                return CreatedAtAction(nameof(GetById), new { id = newDiscussion.Id }, newDiscussion); // HTTP 201 Created
            }
            catch (ArgumentException ex)
            {
                logger.LogError("Invalid argument: " + ex.Message);
                return BadRequest(ex.Message); // HTTP 400
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to add discussion: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CreateDiscussionDTO discussion)
        {
            try
            {
                if (discussion == null)
                {
                    return BadRequest("Discussion cannot be null"); // HTTP 400
                }

                var updatedDiscussion = await DiscussionService.UpdateAsync(discussion);
                return Ok(updatedDiscussion); // HTTP 200 OK
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError("Discussion not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to update discussion: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await DiscussionService.DeleteAsync(id);
                return NoContent(); // HTTP 204 No Content
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("Discussion not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to delete discussion: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500
            }
        }
    }
}
