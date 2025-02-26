using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserStatus Status { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
       
    }
     public enum UserStatus
     {
        SimpleUser, // יכול רק לצפות
        PendingApproval, // מילא טופס וממתין לאישור
        Approved, // אושר ויכול להתחבר
        LoggedIn, // מחובר ויכול להגיב וליצור דיונים
        Admin // מנהל עם כל ההרשאות
     }

}
