using OgrenciBilgiSistemi.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Entity.Dtos.User.Request
{
    public class LoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public byte UserType { get; set; }
    }
}
