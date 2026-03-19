using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionBook.Domain.Entities
{
    public class CommentsEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Comment { get; set; }
        [Required]
        public int? FanficId { get; set; }
        public FanficEntity? Fanfic { get; set; }
        [Required]
        public int? UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}
