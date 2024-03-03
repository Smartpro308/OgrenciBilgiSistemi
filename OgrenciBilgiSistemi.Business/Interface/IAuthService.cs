using OgrenciBilgiSistemi.Core.Result;
using OgrenciBilgiSistemi.Core.Security.Dto;
using OgrenciBilgiSistemi.Entity.Dtos.User.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Business.Interface
{
    public interface IAuthService
    {
        IDataResult<bool> Login(LoginDto loginDto);
        IDataResult<bool> Register(RegisterDto registerDto);
        IDataResult<AccessToken> CreateToken(int userId, byte userType);

    }
}
