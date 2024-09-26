using BLL.Interfaces;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class SubjectValidations
    {
        private readonly ISubjectService subjectService;

        public SubjectValidations(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        public async Task<bool> SubjectExistsAsync(int subjectId)
        {
            var subject = await subjectService.GetByIdAsync(subjectId);
            return subject != null;
        }
    }
}
