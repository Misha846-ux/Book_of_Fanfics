using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanfictionBook.Domain.Enums;

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
        public UserRole? Role { get; set; } = UserRole.User;
        [Required]
        public string? PasswordHash { get; set; }
        public string? RecoveryTokenHash { get; set; }
        public DateTime? RecoveryTokenLifeTime { get; set; }
        public ICollection<CommentsEntity> Comments { get; set; } 
        public ICollection<FanficEntity> Fanfics { get; set; }
    }
}
