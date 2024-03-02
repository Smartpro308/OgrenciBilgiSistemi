using OgrenciBilgiSistemi.Core.Security.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Core.Security.Helpers
{
    public interface ITokenHelper
    {
        SessionAddDto CreateNewToken(UserDto userDto);
    }
}
