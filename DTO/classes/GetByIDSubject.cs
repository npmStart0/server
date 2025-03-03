using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.classes
{
    public class GetByIDSubjectDTO:SubjectDTO
    {
        public List<GetDiscussionDTO> Discussions { get; set; }
    }
}
