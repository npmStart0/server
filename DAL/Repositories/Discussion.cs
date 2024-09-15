using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;

namespace DAL.Repositories
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly IContext context;
        private readonly ILogger<string> logger;
        public DiscussionRepository(IContext context, ILogger<string> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Discussion> AddAsync(Discussion entity)
        {
            try
            {
                var newEntity = await this.context.Discussions.AddAsync(entity);
                await context.SaveChangesAsync();
                return newEntity.Entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to add Discussion" + ex.Message.ToString());
                return new Discussion();
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                context.Discussions.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to delete Discussion" + ex.Message.ToString());
            }
        }

        public async Task<List<Discussion>> GetAllAsync()
        {
            try
            {
                var discussion = context.Discussions
                    .Include(d => d.Subject)
                    .Include(d => d.User)
                    .Include(c => c.Comments);

                if (discussion != null)
                {
                    return await discussion.ToListAsync();
                }
                else
                {
                    throw new Exception("Discussions not found");
                }

            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Discussions" + ex.Message.ToString());
                return new List<Discussion>();
            }
        }

        public async Task<Discussion> GetByIdAsync(int Id)
        {
            try
            {
                var discussion = context.Discussions
                .Include(d => d.Subject)
                .Include(d => d.User)
                .Include(c => c.Comments)
                .FirstOrDefault(d => d.Id == Id);

                if (discussion == null)
                {
                    logger.LogError("The Discussion is null");
                    throw new Exception("The Discussion is null");
                }
                return discussion;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Discussion" + ex.Message.ToString());
                return new Discussion();
            }
        }

        public async Task<Discussion> UpdateAsync(Discussion entity)
        {
            try
            {
                var DiscussionToUpdate = await GetByIdAsync(entity.Id);
                if (DiscussionToUpdate == null)
                {
                    logger.LogError("the Id is not exit");
                    return new Discussion();
                }
                await DeleteAsync(entity.Id);
                await AddAsync(entity);

                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to update Discussion" + ex.Message.ToString());
                return new Discussion();
            }
        }
    }
}
