using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanfictionBook.Application.Interfaces.Helpers;

namespace FanfictionBook.Infrastructure.Helpers
{
    public class HashHelper : IHashHelper
    {
        public bool Check(string password, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
            }
            catch (Exception ex)
            {
                throw new Exception("IHashHelper: Problem with Check");
            }
        }

        public string HashPassword(string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            }
            catch (Exception ex)
            {
                throw new Exception("IHashHelper: Problem with HashPassword");
            }
        }
    }
}
