using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionBook.Domain.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        public ICollection<CommentsEntity> Comments { get; set; } 
        public ICollection<FanficEntity> Fanfics { get; set; }
    }
}
