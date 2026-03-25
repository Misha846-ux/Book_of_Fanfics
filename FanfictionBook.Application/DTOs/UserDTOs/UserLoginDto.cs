using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanfictionBook.Application.DTOs.UserDTOs
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
