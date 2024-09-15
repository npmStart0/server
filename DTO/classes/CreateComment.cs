using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.classes
{
    public class CreateCommentDTO : CommentDTO
    {

        public int UserId { get; set; }
        public int DiscussionId { get; set; }

    }
}
