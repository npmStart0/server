using AutoMapper;
using DTO.classes;
using Microsoft.Extensions.Logging;
using DAL.Interfaces;
using DAL.Models;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IMapper mapper;
        private readonly ILogger<string> logger;

        public UserService(IUserRepository UserRepository, IMapper mapper, ILogger<string> logger)
        {
            this.UserRepository = UserRepository;
            this.logger = logger;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DTO.classes.Mapper>();
            });
            this.mapper = config.CreateMapper();
            //this.mapper = mapper;
        }

        public async Task<UserDTO> AddNewUserAsync(UserDTO e)
        {
            try
            {
                e.Id = 0;
                var map = mapper.Map<UserDTO, User>(e);
                var answer = await UserRepository.AddAsync(map);
                UserDTO v = mapper.Map<User, UserDTO>(answer);
                return v;
            }
            catch (Exception ex)
            {
                logger.LogError("faild to add User in the service" + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await UserRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to delete User in the service" + ex.Message);
                throw;
            }
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var answer= await UserRepository.GetAllAsync();
                return mapper.Map<List<UserDTO>>(answer);    
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get all Users in the service" + ex.Message);
                throw;
            }
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            try
            {
                var answer= await UserRepository.GetByIdAsync(id);
                return mapper.Map<UserDTO>(answer);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get User in the service" + ex.Message);
                throw;
            }
        }

        public async Task<UserDTO> GetByEmailAndByPasswordAsync(string email, string password)
        {
            try
            {
                var answer = await UserRepository.GetByEmailAndByPasswordAsync(email, password);
                return mapper.Map<UserDTO>(answer);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get User in the service" + ex.Message);
                throw;
            }
        }

        public async Task<UserDTO> UpdateAsync(UserDTO e)
        {
            try
            {
                var map= mapper.Map<UserDTO, User>(e);
                var answer= await UserRepository.UpdateAsync(map);
                return mapper.Map<User, UserDTO>(answer);
            }
            catch(Exception ex)
            {
                logger.LogError("faild to update User in the service" + ex.Message);
                throw;
            }
        }
    }
}
