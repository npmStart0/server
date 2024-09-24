using DAL.Models;
using DTO.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task<GetCommentDTO> AddNewCommentAsync(CreateCommentDTO Comment);
        Task DeleteAsync(int id);
        Task<GetCommentDTO> GetByIdAsync(int id);
        Task<List<GetCommentDTO>> GetAllCommentsAsync();
        Task<GetCommentDTO> UpdateAsync(CreateCommentDTO Comment);
        Task<List<GetCommentDTO>> GetCommentsByDiscussionIdAsync(int discussionId);
    }
}
