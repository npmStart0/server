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
    public class CommentController : ControllerBase
    {
        readonly ICommentService commentService;
        readonly IDiscussionService discussionService;
        readonly ILogger<CommentController> logger;
        readonly UserValidations userValidations; 
        readonly DiscussionValidations disucssionValidations;

        public CommentController(ICommentService cService,
            IDiscussionService dService, ILogger<CommentController> logService, UserValidations usValidate, DiscussionValidations disValidations)
        {
            commentService = cService;
            discussionService = dService;
            logger = logService;
            userValidations = usValidate; 
            disucssionValidations = disValidations;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await commentService.GetAllCommentsAsync();
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
                var comment = await commentService.GetByIdAsync(id);
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
        public async Task<IActionResult> Add([FromBody] CreateCommentDTO newComment)
        {
            try
            {
                if (newComment == null)
                {
                    return BadRequest("Comment cannot be null"); // HTTP 400 Bad Request
                }

                var userExists = await userValidations.UserExistsAsync(newComment.UserId);
                if (!userExists)
                {
                    return BadRequest("User does not exist."); // HTTP 400 Bad Request
                }

                var discussionExists = await disucssionValidations.ExistsAsync(newComment.DiscussionId);
                if (!discussionExists)
                {
                    return BadRequest("Discussion does not exist."); // HTTP 400 Bad Request
                }

                await commentService.AddNewCommentAsync(newComment);
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
        public async Task<IActionResult> Update([FromBody] CreateCommentDTO comment)
        {
            try
            {
                if (comment == null)
                {
                    return BadRequest("Comment cannot be null"); // HTTP 400 Bad Request
                }

                var updatedComment = await commentService.UpdateAsync(comment);
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
                    await commentService.DeleteAsync(id);
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

        [HttpGet("discussion/{discussionId}")]
        public async Task<IActionResult> GetByDiscussionId(int discussionId)
        {
            try
            {
                var comments = await commentService.GetCommentsByDiscussionIdAsync(discussionId);

                if (comments == null || comments.Count == 0)
                {
                    return NotFound($"No comments found for discussion with ID {discussionId}");
                }

                return Ok(comments); 
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get comments for discussion with ID {discussionId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error"); 
            }
        }


    }

}
