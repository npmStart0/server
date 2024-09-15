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
    public class DiscussionController : ControllerBase
    {
        readonly IDiscussionService DiscussionService;
        private ILogger<string> logger;
        public DiscussionController(IDiscussionService service, ILogger<string> logger)
        {
            DiscussionService = service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<List<GetDiscussionDTO>> GetAll()
        {
            try
            {
                return await DiscussionService.GetAllDiscussionsAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get all "+ex.Message);
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<GetDiscussionDTO> GetbyId(int id)
        {
            try
            {
                return await DiscussionService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"fail to get Discussion with this id {ex.Message}");
                return null;
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Add(CreateDiscussionDTO newDiscussion)
        {
            try
            {
                await DiscussionService.AddNewDiscussionAsync(newDiscussion);
            }
            catch (Exception ex)
            {
                logger.LogError("faild in api to add Discussion" + ex.Message);
            }
        }
        [HttpPut]
        public async Task<GetDiscussionDTO> Update(CreateDiscussionDTO e)
        {
            try
            {
                return await DiscussionService.UpdateAsync(e);
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
                await DiscussionService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("failed to delete "+ex.Message);
            }
        }
    }
}
