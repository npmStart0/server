using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IContext context;
        private readonly ILogger<string> logger;
        public CommentRepository(IContext context, ILogger<string> logger) {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Comment> AddAsync(Comment entity)
        {
            try
            {
                var newEntity = await this.context.Comments.AddAsync(entity);
                await context.SaveChangesAsync();
                return newEntity.Entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to add Comment" + ex.Message.ToString());
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                context.Comments.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to delete Comment" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            try
            {
                var comments = context.Comments
                    .Include(c => c.User)
                    .Include(c => c.Discussion);

                if (comments != null)
                {
                    return await comments.ToListAsync();
                }
                else
                {
                    throw new Exception("Comments not found");
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Comments" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<Comment> GetByIdAsync(int Id)
        {
            try
            {
                var comment = context.Comments
                .Include(c => c.User)
                .Include(c => c.Discussion) 
                .FirstOrDefault(c => c.Id == Id);

                if (comment != null)
                {

                    return comment;
                }
                else 
                {
                    logger.LogError("The Comment is null");
                    throw new ArgumentNullException(nameof(comment), "The Comment is null");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Comment" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<Comment> UpdateAsync(Comment entity)
        {
            try
            {
                var CommentToUpdate = await GetByIdAsync(entity.Id);
                if (CommentToUpdate == null)
                {
                    logger.LogError("the Id is not exit");
                    throw new ArgumentNullException(nameof(CommentToUpdate), "The Id is not exit");
                }
                await DeleteAsync(entity.Id);
                await AddAsync(entity);


                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to update Comment" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<List<Comment>> GetCommentsByDiscussionIdAsync(int discussionId)
        {
            return await context.Comments
             .Include(c => c.User)  
             .Include(c => c.Discussion)
             .Where(c => c.DiscussionId == discussionId)
             .ToListAsync();
        }
    }
}
