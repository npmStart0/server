using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject> GetByIdAsync(int id);
        Task<Subject> UpdateAsync(Subject entity);
        Task<Subject> AddAsync(Subject entity);
        Task DeleteAsync(int id);
        int CountOfDiscussionsForSubject(int subjectId);
        Task<List<Discussion>> ListOfDiscussionsForSubject(int subjectId);

    }
}
