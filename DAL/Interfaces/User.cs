using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAndByPasswordAsync(string email, string password);
        Task<User> UpdateAsync(User entity);
        Task<User> AddAsync(User entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<User>> GetPendingUsersAsync();
    }
}
