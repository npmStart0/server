using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDiscussionRepository
    {
        Task<List<Discussion>> GetAllAsync();
        Task<List<Comment>> GetListCommentByIdDiscussionAsync(int id);
        Task<Discussion> GetByIdAsync(int id);
        Task<Discussion> UpdateAsync(Discussion entity);
        Task<Discussion> AddAsync(Discussion entity);
        Task DeleteAsync(int id);
    }
}
