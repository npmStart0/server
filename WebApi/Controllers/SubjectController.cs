using DTO.classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        readonly ISubjectService SubjectService;
        private ILogger<string> logger;
        public SubjectController(ISubjectService service, ILogger<string> logger)
        {
            SubjectService = service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subjects = await SubjectService.GetAllSubjectsAsync();
                return Ok(subjects); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to get all subjects: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var subject = await SubjectService.GetByIdAsync(id);
                if (subject == null)
                {
                    return NotFound($"Subject with ID {id} not found"); // HTTP 404 Not Found
                }
                return Ok(subject); // HTTP 200 OK
            }
            catch (ArgumentException ex)
            {
                logger.LogError($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message); // HTTP 400 Bad Request
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get subject with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error"); // HTTP 500
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubjectDTO newSubject)
        {
            try
            {
                if (newSubject == null)
                {
                    return BadRequest("Subject cannot be null"); // HTTP 400 Bad Request
                }

                await SubjectService.AddNewSubjectAsync(newSubject);
                return CreatedAtAction(nameof(GetById), new { id = newSubject.Id }, newSubject); // HTTP 201 Created
            }
            catch (ArgumentException ex)
            {
                logger.LogError("Invalid argument: " + ex.Message);
                return BadRequest(ex.Message); // HTTP 400 Bad Request
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to add subject: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SubjectDTO subject)
        {
            try
            {
                if (subject == null)
                {
                    return BadRequest("Subject cannot be null"); // HTTP 400 Bad Request
                }

                var updatedSubject = await SubjectService.UpdateAsync(subject);
                return Ok(updatedSubject); // HTTP 200 OK
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError("Subject not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to update subject: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await SubjectService.DeleteAsync(id);
                return NoContent(); // HTTP 204 No Content
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("Subject not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to delete subject: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

    }
}
