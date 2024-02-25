using OgrenciBilgiSistemi.Core.Result;
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
        IDataResult<bool> LoginForUser(LoginDto loginDto);
        IDataResult<bool> RegisterForUser(RegisterDto registerDto);

    }
}
