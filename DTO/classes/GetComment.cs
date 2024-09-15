using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.classes
{
    public class GetCommentDTO : CommentDTO
    {

        public string User { get; set; }
        public string Discussion { get; set; }
    }
}
