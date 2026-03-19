using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionBook.Domain.Entities
{
    public class FanficEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime? CreatedAt { get; set; }
        [Required]
        [Range(0, 100)]
        public int? Rating {  get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public UserEntity? User { get; set; }
        public ICollection<GenreEntity> Genres { get; set; }
        public ICollection<CommentsEntity> Comments { get; set; }

    }
}
