using DTO.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISubjectService
    {
        Task<SubjectDTO> AddNewSubjectAsync(SubjectDTO Subject);
        Task DeleteAsync(int id);
        Task<GetByIDSubjectDTO> GetByIdAsync(int id);
        Task<List<GetSubjectDTO>> GetAllSubjectsAsync();
        Task<SubjectDTO> UpdateAsync(SubjectDTO Subject);
    }
}
