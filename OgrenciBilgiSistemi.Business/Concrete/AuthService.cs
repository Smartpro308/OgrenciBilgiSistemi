using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Core.Helpers;
using OgrenciBilgiSistemi.Core.Result;
using OgrenciBilgiSistemi.Entity.Dtos.User.Request;
using OgrenciBilgiSistemi.Entity.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public IDataResult<bool> LoginForUser(LoginDto loginDto)
        {
            try
            {
                if (loginDto.Email == null || loginDto.Password == null)
                {
                    return new ErrorDataResult<bool>(false, "Lütfen tüm bilgilerinizi eksiksiz doldurun.");
                }

                var user = _userService.GetByFilter(x => x.Email == loginDto.Email);

                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip kullanıcı bulunmamaktadır.");
                }

                var verify = HashingHelper.VerifyPasswordHash(loginDto.Password, user.Data.PasswordSalt, user.Data.PasswordHash);

                if (!verify)
                {
                    return new ErrorDataResult<bool>(false, "Şifreniz yanlış!");
                }

                return new SuccessDataResult<bool>(true, "Ok");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }

        public IDataResult<bool> RegisterForUser(RegisterDto registerDto)
        {
            try
            {
                if (registerDto.Name == null || registerDto.Surname == null || registerDto.Email == null || registerDto.Password == null)
                {
                    return new ErrorDataResult<bool>(false, "Lütfen tüm bilgilerinizi eksiksiz doldurun.");
                }

                var checkUser = _userService.GetByFilter(x => x.Email == registerDto.Email);

                if (checkUser != null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip bir kullanıcı zaten var.");
                }

                if (registerDto.Password.Contains(registerDto.Name.Trim()))
                {
                    return new ErrorDataResult<bool>(false, "Parolanız adınızı içeremez.");
                }

                if (registerDto.Password.Length < 5)
                {
                    return new ErrorDataResult<bool>(false, "Parolanız en az 5 karakterden oluşmalıdır.");
                }

                if (registerDto.Password.Contains(' '))
                {
                    return new ErrorDataResult<bool>(false, "Parolanız boşluk içeremez.");
                }

                if (registerDto.Password != registerDto.ControlPassword)
                {
                    return new ErrorDataResult<bool>(false, "Parolalar birbiri ile uyuşmuyor.");
                }

                byte[] passwordSalt, passwordHash;

                HashingHelper.CreatePasswordHash(registerDto.Password, out passwordSalt, out passwordHash);

                User user = new User
                {
                    Name = registerDto.Name,
                    Surname = registerDto.Surname,
                    CreatedDate = DateTime.UtcNow,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = registerDto.Email,
                    IsActive = true
                };

                _userService.Add(user);

                return new SuccessDataResult<bool>(true, "Kayıt gerçekleşti!");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }
    }
}
