using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> UpdateAsync(Comment entity);
        Task<Comment> AddAsync(Comment entity);
        Task DeleteAsync(int id);
        Task<List<Comment>> GetCommentsByDiscussionIdAsync(int discussionId);
    }
}
