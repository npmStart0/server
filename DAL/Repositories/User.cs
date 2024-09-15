using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IContext context;
        private readonly ILogger<string> logger;
        private readonly MyDbContext myDbContext;
        public UserRepository(IContext context, ILogger<string> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<User> AddAsync(User entity)
        {
            try
            {
                var newEntity = await context.Users.AddAsync(entity);
                await context.SaveChangesAsync();
                return newEntity.Entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to add User" + ex.Message.ToString());
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                context.Users.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to delete User" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                return await context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Users" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<User> GetByIdAsync(int Id)
        {
            try
            {
                var entity = await context.Users.FirstOrDefaultAsync(p => p.Id == Id);
                if (entity == null)
                {
                    logger.LogError("The User is null");
                    throw new ArgumentNullException(nameof(entity), "The User is null");
                }
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get User" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<User> GetByEmailAndByPasswordAsync(string email, string password)
        {
            try
            {
                var entity = await context.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
                if (entity == null)
                {
                    logger.LogError("The User is null");
                    throw new ArgumentNullException(nameof(entity), "The User is null");
                }
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get User" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<User> UpdateAsync(User entity)
        {
            try
            {
                var UserToUpdate = await GetByIdAsync(entity.Id);
                if (UserToUpdate == null)
                {
                    logger.LogError("the Id is not exit");
                    throw new ArgumentNullException(nameof(UserToUpdate), "The User is null");
                }
                await DeleteAsync(entity.Id);
                await AddAsync(entity);


                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to update User" + ex.Message.ToString());
                throw;
            }
        }
    }
}
