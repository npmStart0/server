using BLL.Interfaces;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class UserValidations
    {
        readonly IUserService userService;

        public UserValidations(IUserService user)
        {
            userService = user;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            var user = await userService.GetByIdAsync(userId);
            return user != null;
        }
    }
}
