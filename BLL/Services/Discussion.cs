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
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository DiscussionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<string> logger;

        public DiscussionService(IDiscussionRepository DiscussionRepository, IMapper mapper, ILogger<string> logger)
        {
            this.DiscussionRepository = DiscussionRepository;
            this.logger = logger;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DTO.classes.Mapper>();
            });
            this.mapper = config.CreateMapper();
        }

        public async Task<GetDiscussionDTO> AddNewDiscussionAsync(CreateDiscussionDTO e)
        {
            try
            {
                e.Id = 0;
                var map = mapper.Map<Discussion>(e);
                var answer=await DiscussionRepository.AddAsync(map);
                return mapper.Map<GetDiscussionDTO>(answer);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to add Discussion in the service" + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await DiscussionRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to delete Discussion in the service" + ex.Message);
                throw;
            }
        }

        public async Task<List<GetDiscussionDTO>> GetAllDiscussionsAsync()
        {
            try
            {
                var answer= await DiscussionRepository.GetAllAsync();
                return mapper.Map<List<GetDiscussionDTO>>(answer);    
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get all Discussions in the service" + ex.Message);
                throw;
            }
        }

        public async Task<GetDiscussionDTO> GetByIdAsync(int id)
        {
            try
            {
                var answer= await DiscussionRepository.GetByIdAsync(id);
                return mapper.Map<GetDiscussionDTO>(answer);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get Discussion in the service" + ex.Message);
                throw;
            }
        }

        public async Task<List<GetCommentDTO>> GetListCommentByIdDiscussionAsync(int id)
        {
           var answer = await DiscussionRepository.GetListCommentByIdDiscussionAsync(id);
            return mapper.Map<List<GetCommentDTO>>(answer);
        }

        public async Task<GetDiscussionDTO> UpdateAsync(CreateDiscussionDTO e)
        {
            try
            {
                var map= mapper.Map<Discussion>(e);
                var answer= await DiscussionRepository.UpdateAsync(map);
                return mapper.Map<GetDiscussionDTO>(answer);
            }
            catch(Exception ex)
            {
                logger.LogError("faild to update Discussion in the service" + ex.Message);
                throw;
            }
        }
    }
}
