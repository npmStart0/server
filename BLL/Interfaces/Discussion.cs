using DTO.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDiscussionService
    {
        Task<GetDiscussionDTO> AddNewDiscussionAsync(CreateDiscussionDTO Discussion);
        Task DeleteAsync(int id);
        Task<GetDiscussionDTO> GetByIdAsync(int id);
        Task<List<GetCommentDTO>> GetListCommentByIdDiscussionAsync(int id);
        Task<List<GetDiscussionDTO>> GetAllDiscussionsAsync();
        Task<GetDiscussionDTO> UpdateAsync(CreateDiscussionDTO Discussion);
    }
}
