using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionBook.Domain.Entities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public ICollection<FanficEntity> Fanfic { get; set; }
    }
}
