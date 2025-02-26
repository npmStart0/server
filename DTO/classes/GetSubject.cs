using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.classes
{
    public class GetSubjectDTO : SubjectDTO
    {
        public int DiscussionsCount { get; set; }
    }
}
