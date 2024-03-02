using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Core.Result;
using OgrenciBilgiSistemi.Core.Security.Dto;
using OgrenciBilgiSistemi.Core.Security.Helpers;
using OgrenciBilgiSistemi.Dal.Interface;
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
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserSessionDal _userSessionDal;

        public AuthService(IUserService userService, ITokenHelper tokenHelper, IUserSessionDal userSessionDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userSessionDal = userSessionDal;
        }

        public IDataResult<AccessToken> CreateToken(int userId)
        {
            try
            {
                var user = _userService.GetById(userId);

                var userDto = new UserDto()
                {
                    Id = user.Data.Id,
                    Email = user.Data.Email
                };

                //aşağıdaki sessionAddDto içinde hashlenmiş token değeri ve userId tutmaktadır
                var sessionAddDto = _tokenHelper.CreateNewToken(userDto);

                var userSession = new UserSession()
                {
                    Token = sessionAddDto.TokenString,
                    UserId = sessionAddDto.UserId,
                    CreatedDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(1)
                };

                _userSessionDal.Add(userSession);

                var accessToken = new AccessToken()
                {
                    Expiration = userSession.ExpireDate,
                    Token = userSession.Token
                };

                return new SuccessDataResult<AccessToken>(accessToken, "Ok");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<AccessToken>(new AccessToken(), e.Message);
            }
          
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
