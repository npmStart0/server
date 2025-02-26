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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository CommentRepository;
        private readonly IMapper mapper;
        private readonly ILogger<string> logger;

        public CommentService(ICommentRepository CommentRepository, IMapper mapper, ILogger<string> logger)
        {
            this.CommentRepository = CommentRepository;
            this.logger = logger;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DTO.classes.Mapper>();
            });
            this.mapper = config.CreateMapper();
        }

        public async Task<GetCommentDTO> AddNewCommentAsync(CreateCommentDTO e)
        {
            try
            {
                e.Id = 0;
                var map = mapper.Map<Comment>(e);
                var answer=await CommentRepository.AddAsync(map);
                return mapper.Map<GetCommentDTO>(answer);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to add Comment in the service" + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await CommentRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to delete Comment in the service" + ex.Message);
                throw;
            }
        }

        public async Task<List<GetCommentDTO>> GetAllCommentsAsync()
        {
            try
            {
                var answer= await CommentRepository.GetAllAsync();
                return mapper.Map<List<GetCommentDTO>>(answer);    
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get all Comments in the service" + ex.Message);
                throw;
            }
        }

        public async Task<GetCommentDTO> GetByIdAsync(int id)
        {
            try
            {
                var answer= await CommentRepository.GetByIdAsync(id);
                return mapper.Map<GetCommentDTO>(answer);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get Comment in the service" + ex.Message);
                throw;
            }
        }

        public async Task<GetCommentDTO> UpdateAsync(CreateCommentDTO e)
        {
            try
            {
                var map= mapper.Map<Comment>(e);
                var answer= await CommentRepository.UpdateAsync(map);
                return mapper.Map<GetCommentDTO>(answer);
            }
            catch(Exception ex)
            {
                logger.LogError("faild to update Comment in the service" + ex.Message);
                throw;
            }
        }

        public async Task<List<GetCommentDTO>> GetCommentsByDiscussionIdAsync(int discussionId)
        {
            try
            {
                var comments = await CommentRepository.GetCommentsByDiscussionIdAsync(discussionId);
                return mapper.Map<List<GetCommentDTO>>(comments);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get comments by discussionId {discussionId}: {ex.Message}");
                throw;
            }
        }

    }
}
