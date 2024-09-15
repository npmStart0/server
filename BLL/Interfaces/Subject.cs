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
        Task<SubjectDTO> GetByIdAsync(int id);
        Task<List<SubjectDTO>> GetAllSubjectsAsync();
        Task<SubjectDTO> UpdateAsync(SubjectDTO Subject);
    }
}
