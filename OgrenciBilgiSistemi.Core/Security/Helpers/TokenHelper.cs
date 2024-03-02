using OgrenciBilgiSistemi.Core.Security.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Core.Security.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        public SessionAddDto CreateNewToken(UserDto userDto)
        {
            Guid guid = Guid.NewGuid();

            var tokenString = HashString(guid.ToString(), "p4rm4k4r4sit3rl1k");

            var session = new SessionAddDto()
            {
                TokenString = tokenString,
                UserId = userDto.Id
            };

            return session;
        }

        private string HashString(string token, string salt)
        {
            using (var sha = new HMACSHA256())
            {
                byte[] tokenBytes = Encoding.UTF8.GetBytes(token + salt);
                byte[] hashBytes = sha.ComputeHash(tokenBytes);

                string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }


    }
}
