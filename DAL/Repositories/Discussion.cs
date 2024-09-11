using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw;
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
                throw;
            }
        }

        public async Task<List<Discussion>> GetAllAsync()
        {
            try
            {
                return await context.Discussions.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Discussions" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<Discussion> GetByIdAsync(int Id)
        {
            try
            {
                var entity = await context.Discussions.FirstOrDefaultAsync(p => p.Id == Id);
                if (entity == null)
                {
                    logger.LogError("The Discussion is null");
                    throw new ArgumentNullException(nameof(entity), "The Discussion is null");
                }
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Discussion" + ex.Message.ToString());
                throw;
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
                    throw new ArgumentNullException(nameof(DiscussionToUpdate), "The Id is not exit");
                }
                await DeleteAsync(entity.Id);
                await AddAsync(entity);
                //DiscussionToUpdate.Name = entity.Name;
                //DiscussionToUpdate.DepartmentCode = entity.DepartmentCode;
                //DiscussionToUpdate.CompanyCode = entity.CompanyCode;
                //DiscussionToUpdate.Price = entity.Price;
                //DiscussionToUpdate.Description = entity.Description;
                //DiscussionToUpdate.Picture = entity.Picture;

                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to update Discussion" + ex.Message.ToString());
                throw;
            }
        }
    }
}
