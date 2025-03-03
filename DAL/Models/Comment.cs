using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        
        public virtual int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        [Required]
        public virtual int DiscussionId { get; set; }
        [ForeignKey("DiscussionId")]
        public virtual Discussion Discussion { get; set; }

        public DateTime Date { get; set; }
    }
}
