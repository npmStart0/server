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
        public async Task<List<GetCommentDTO>> GetAll()
        {
            try
            {
                return await CommentService.GetAllCommentsAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get all "+ex.Message);
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<GetCommentDTO> GetbyId(int id)
        {
            try
            {
                return await CommentService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"fail to get Comment with this id {ex.Message}");
                return null;
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Add(CreateCommentDTO newComment)
        {
            try
            {
                await CommentService.AddNewCommentAsync(newComment);
            }
            catch (Exception ex)
            {
                logger.LogError("faild in api to add Comment" + ex.Message);
            }
        }
        [HttpPut]
        public async Task<GetCommentDTO> Update(CreateCommentDTO e)
        {
            try
            {
                return await CommentService.UpdateAsync(e);
            }
            catch (Exception ex)
            {
                logger.LogError("failed to update "+ex.Message);
                return null;
            }
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await CommentService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("failed to delete "+ex.Message);
            }
        }
    }
}
