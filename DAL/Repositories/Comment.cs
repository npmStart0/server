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
        public CommentRepository(IContext context, ILogger<string> logger)
        {
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
                return new Comment();
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
                return new List<Comment>();
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
                    throw new Exception("Comment not found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Comment" + ex.Message.ToString());
                return new Comment();
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
                    return new Comment();
                }
                await DeleteAsync(entity.Id);
                await AddAsync(entity);


                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to update Comment" + ex.Message.ToString());
                return new Comment();
            }
        }
    }
}
