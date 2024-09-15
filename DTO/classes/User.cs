using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.classes
{
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
