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
        private ILogger<string> logger;
        public UserController(IUserService service, ILogger<string> logger)
        {
            UserService = service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<List<UserDTO>> GetAll()
        {
            try
            {
                return await UserService.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get all "+ex.Message);
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<UserDTO> GetbyId(int id)
        {
            try
            {
                return await UserService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError($"fail to get User with this id {ex.Message}");
                return null;
            }
        }

        [HttpGet("{email}/{password}")]
        public async Task<UserDTO> GetByEmailAndPassword(string email, string password)
        {
            try
            {
                return await UserService.GetByEmailAndByPasswordAsync(email, password);
            }
            catch (Exception ex)
            {
                logger.LogError($"fail to get User with this id {ex.Message}");
                return null;
            }
        }
        // POST api/<ValuesController>
        [HttpPost]
        public async Task Add(UserDTO newUser)
        {
            try
            {
                await UserService.AddNewUserAsync(newUser);
            }
            catch (Exception ex)
            {
                logger.LogError("faild in api to add User" + ex.Message);
            }
        }
        [HttpPut]
        public async Task<UserDTO> Update(UserDTO e)
        {
            try
            {
                return await UserService.UpdateAsync(e);
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
                await UserService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("failed to delete "+ex.Message);
            }
        }
    }
}
