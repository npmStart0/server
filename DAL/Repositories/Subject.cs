using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;

namespace DAL.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IContext context;
        private readonly ILogger<string> logger;
        public SubjectRepository(IContext context, ILogger<string> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<Subject> AddAsync(Subject entity)
        {
            try
            {
                var newEntity = await this.context.Subjects.AddAsync(entity);
                await context.SaveChangesAsync();
                return newEntity.Entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to add Subject" + ex.Message.ToString());
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                context.Subjects.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to delete Subject" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            try
            {
                return await context.Subjects.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Subjects" + ex.Message.ToString());

                throw new Exception("failed in dal.subject "+ex.Message.ToString());
            }
        }

        public async Task<Subject> GetByIdAsync(int Id)
        {
            try
            {
                var entity = await context.Subjects.FirstOrDefaultAsync(p => p.Id == Id);
                if (entity == null)
                {
                    logger.LogError("The Subject is null");
                    throw new ArgumentNullException(nameof(entity), "The subject is null");
                }
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to get Subject" + ex.Message.ToString());
                throw;
            }
        }

        public async Task<Subject> UpdateAsync(Subject entity)
        {
            try
            {
                var SubjectToUpdate = await GetByIdAsync(entity.Id);
                if (SubjectToUpdate == null)
                {
                    logger.LogError("the Id is not exit");
                    throw new ArgumentNullException(nameof(SubjectToUpdate), "the Id is not exit");
                }
                await DeleteAsync(entity.Id);
                await AddAsync(entity);

                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError("failed to update Subject" + ex.Message.ToString());
                throw;
            }
        }
        public int CountOfDiscussionsForSubject(int subjectId)
        {
            return context.Discussions.Count(d => d.SubjectId == subjectId);
        }
       
        public  Task<List<Discussion>> ListOfDiscussionsForSubject(int subjectId)
        {
            return  context.Discussions
                                .Where(d => d.SubjectId == subjectId)
                                .Include(d => d.User) 
                                .Include(d => d.Comments) 
                                .ToListAsync();
        }

    }
}
