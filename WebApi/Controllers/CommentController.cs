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
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IDiscussionService _discussionService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService, IUserService userService, IDiscussionService discussionService, ILogger<CommentController> logger)
        {
            _userService = userService;
            _commentService = commentService;
            _discussionService = discussionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _commentService.GetAllCommentsAsync();
                return Ok(comments); // HTTP 200 OK
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all comments: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var comment = await _commentService.GetByIdAsync(id);
                if (comment == null)
                {
                    return NotFound($"Comment with ID {id} not found"); // HTTP 404 Not Found
                }
                return Ok(comment); // HTTP 200 OK
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Invalid argument: {ex.Message}");
                return BadRequest(ex.Message); // HTTP 400 Bad Request
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get comment with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCommentDTO newComment)
        {
            try
            {
                if (newComment == null)
                {
                    return BadRequest("Comment cannot be null"); // HTTP 400 Bad Request
                }

                // check if user and discussion exsits
                var userExists = await _userService.GetByIdAsync(newComment.UserId);
                if (userExists==null)
                {
                    return BadRequest("User does not exist."); // HTTP 400 Bad Request
                }
                var discussionExists = await _discussionService.GetByIdAsync(newComment.DiscussionId);
                if (discussionExists == null)
                {
                    return BadRequest("discussion does not exist."); // HTTP 400 Bad Request
                }

                await _commentService.AddNewCommentAsync(newComment);
                return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment); // HTTP 201 Created
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Invalid argument: " + ex.Message);
                return BadRequest(ex.Message); // HTTP 400 Bad Request
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add comment: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CreateCommentDTO comment)
        {
            try
            {
                if (comment == null)
                {
                    return BadRequest("Comment cannot be null"); // HTTP 400 Bad Request
                }

                var updatedComment = await _commentService.UpdateAsync(comment);
                return Ok(updatedComment); // HTTP 200 OK
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError("Comment not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update comment: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commentService.DeleteAsync(id);
                return NoContent(); // HTTP 204 No Content
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Comment not found: " + ex.Message);
                return NotFound(ex.Message); // HTTP 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete comment: " + ex.Message);
                return StatusCode(500, "Internal Server Error"); // HTTP 500 Internal Server Error
            }
        }
    }
}
