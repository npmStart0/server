using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.classes
{
    public class GetDiscussionDTO : DiscussionDTO
    {

        public string User { get; set; }
        public int CountOfComments { get; set; }
        public string Subject { get; set; }

    }
}
