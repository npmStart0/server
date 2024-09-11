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
    public class CommentController : ControllerBase
    {
        readonly ICommentService CommentService;
        private ILogger<string> logger;
        public CommentController(ICommentService service, ILogger<string> logger)
        {
            CommentService = service;
            this.logger = logger;
        }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                try
                {
                    var comments = await CommentService.GetAllCommentsAsync();
                    return Ok(comments); // HTTP 200 OK
                }
                catch (Exception ex)
                {
                    logger.LogError("Failed to get all comments: " + ex.Message);
                    return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                try
                {
                    var comment = await CommentService.GetByIdAsync(id);
                    if (comment == null)
                    {
                        return NotFound($"Comment with ID {id} not found"); // HTTP 404 Not Found
                    }
                    return Ok(comment); // HTTP 200 OK
                }
                catch (ArgumentException ex)
                {
                    logger.LogError($"Invalid argument: {ex.Message}");
                    return BadRequest(ex.Message); // HTTP 400 Bad Request
                }
                catch (Exception ex)
                {
                    logger.LogError($"Failed to get comment with ID {id}: {ex.Message}");
                    return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
                }
            }

            [HttpPost]
            public async Task<IActionResult> Add([FromBody] CommentDto newComment)
            {
                try
                {
                    if (newComment == null)
                    {
                        return BadRequest("Comment cannot be null"); // HTTP 400 Bad Request
                    }

                    await CommentService.AddNewCommentAsync(newComment);
                    return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment); // HTTP 201 Created
                }
                catch (ArgumentException ex)
                {
                    logger.LogError("Invalid argument: " + ex.Message);
                    return BadRequest(ex.Message); // HTTP 400 Bad Request
                }
                catch (Exception ex)
                {
                    logger.LogError("Failed to add comment: " + ex.Message);
                    return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
                }
            }

            [HttpPut]
            public async Task<IActionResult> Update([FromBody] CommentDto comment)
            {
                try
                {
                    if (comment == null)
                    {
                        return BadRequest("Comment cannot be null"); // HTTP 400 Bad Request
                    }

                    var updatedComment = await CommentService.UpdateAsync(comment);
                    return Ok(updatedComment); // HTTP 200 OK
                }
                catch (KeyNotFoundException ex)
                {
                    logger.LogError("Comment not found: " + ex.Message);
                    return NotFound(ex.Message); // HTTP 404 Not Found
                }
                catch (Exception ex)
                {
                    logger.LogError("Failed to update comment: " + ex.Message);
                    return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    await CommentService.DeleteAsync(id);
                    return NoContent(); // HTTP 204 No Content
                }
                catch (ArgumentNullException ex)
                {
                    logger.LogError("Comment not found: " + ex.Message);
                    return NotFound(ex.Message); // HTTP 404 Not Found
                }
                catch (Exception ex)
                {
                    logger.LogError("Failed to delete comment: " + ex.Message);
                    return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
                }
            }
        }

    }
