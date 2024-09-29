using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL.Validations
{
    public class DiscussionValidations
    {
        private readonly IDiscussionService discussionService;

        public DiscussionValidations(IDiscussionService discussionService)
        {
            this.discussionService = discussionService;
        }

        public async Task<bool> ExistsAsync(int discussionId)
        {
            var discussion = await discussionService.GetByIdAsync(discussionId);
            return discussion != null;
        }
    }
}
