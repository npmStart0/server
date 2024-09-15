using DTO.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> AddNewUserAsync(UserDTO User);
        Task DeleteAsync(int id);
        Task<UserDTO> GetByIdAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetByEmailAndByPasswordAsync(string Email, string Password);
        Task<UserDTO> UpdateAsync(UserDTO User);
    }
}
