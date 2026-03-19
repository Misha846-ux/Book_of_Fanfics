using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionBook.Application.DTOs.UserDTOs
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<int> Fanfics { get; set; }
    }
}
