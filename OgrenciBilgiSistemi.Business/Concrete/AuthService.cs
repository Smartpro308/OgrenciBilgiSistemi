using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Core.Result;
using OgrenciBilgiSistemi.Core.Security.Dto;
using OgrenciBilgiSistemi.Core.Security.Helpers;
using OgrenciBilgiSistemi.Core.Utilities.Enum;
using OgrenciBilgiSistemi.Dal.Interface;
using OgrenciBilgiSistemi.Entity.Dtos.User.Request;
using OgrenciBilgiSistemi.Entity.Entitites;

namespace OgrenciBilgiSistemi.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly ILecturerService _lecturerService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserSessionDal _userSessionDal;

        public AuthService(IUserService userService, ITokenHelper tokenHelper, IUserSessionDal userSessionDal, IStudentService studentService, ILecturerService lecturerService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userSessionDal = userSessionDal;
            _studentService = studentService;
            _lecturerService = lecturerService;
        }

        public IDataResult<AccessToken> CreateToken(int userId, byte userType)
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

                if (userType < 0 || userType > 2)
                {
                    return new ErrorDataResult<AccessToken>(new AccessToken(), "Kullanıcı tipi bulunamadı");
                }


                var userSession = new UserSession()
                {
                    Token = sessionAddDto.TokenString,
                    ApplicationUserId = sessionAddDto.UserId,
                    CreatedDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(1),
                    ApplicationUserType = userType
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

        public IDataResult<bool> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto.Email == null || loginDto.Password == null)
                {
                    return new ErrorDataResult<bool>(false, "Lütfen tüm bilgilerinizi eksiksiz doldurun.");
                }

                if (loginDto.UserType < 0 || loginDto.UserType> 2)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı tipi bulunamadı");
                }


                IDataResult<bool> result;

                if (loginDto.UserType == (byte)UserTypeEnum.User)
                {
                    var user = _userService.GetByFilter(x => x.Email == loginDto.Email).Data;

                    result = CheckUserAndCreateToken(true, loginDto, null, user);
                }
                else if (loginDto.UserType == (byte)UserTypeEnum.Student)
                {
                    var student = _studentService.GetByFilter(x => x.Email == loginDto.Email).Data;

                    result = CheckStudentAndCreateToken(true, loginDto, null, student);
                }
                else
                {
                    var lecturer = _lecturerService.GetByFilter(x => x.Email == loginDto.Email).Data;

                    result = CheckLecturerAndCreateToken(true, loginDto, null, lecturer);
                }

                return result;
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }

        private IDataResult<bool> CheckUserAndCreateToken(bool isLogin, LoginDto loginDto = null, RegisterDto registerDto = null, User user = null, byte[] passwordSalt = null, byte[] passwordHash = null)
        {
            if (isLogin)
            {
                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip kullanıcı bulunmamaktadır.");
                }

                var verify = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordSalt, user.PasswordHash);

                if (!verify)
                {
                    return new ErrorDataResult<bool>(false, "Şifreniz yanlış!");
                }

                var createToken = CreateToken(user.Id, loginDto.UserType);

                if (createToken.Success == false)
                {
                    return new ErrorDataResult<bool>(false, createToken.Message);
                }

            }
            else
            {
                var checkUser = _userService.GetByFilter(x => x.Email == registerDto.Email);

                if (checkUser != null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip bir kullanıcı zaten var.");
                }


                User newUser = new User
                {
                    Name = registerDto.Name,
                    Surname = registerDto.Surname,
                    CreatedDate = DateTime.UtcNow,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = registerDto.Email,
                    IsActive = true
                };

                _userService.Add(newUser);

                var createToken = CreateToken(newUser.Id, registerDto.UserType);

                if (createToken.Success == false)
                {
                    return new ErrorDataResult<bool>(false, createToken.Message);
                }

            }

            return new SuccessDataResult<bool>(true, "Ok");
        }

        private IDataResult<bool> CheckStudentAndCreateToken(bool isLogin, LoginDto loginDto = null, RegisterDto registerDto = null, Student student = null, byte[] passwordSalt = null, byte[] passwordHash = null)
        {
            if (isLogin)
            {
                if (student == null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip kullanıcı bulunmamaktadır.");
                }

                var verify = HashingHelper.VerifyPasswordHash(loginDto.Password, student.PasswordSalt, student.PasswordHash);

                if (!verify)
                {
                    return new ErrorDataResult<bool>(false, "Şifreniz yanlış!");
                }

                var createToken = CreateToken(student.Id, loginDto.UserType);

                if (createToken.Success == false)
                {
                    return new ErrorDataResult<bool>(false, createToken.Message);
                }

            }
            else
            {
                var checkStudent = _userService.GetByFilter(x => x.Email == registerDto.Email);

                if (checkStudent != null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip bir öğrenci zaten var.");
                }


                Student newStudent = new Student
                {
                    Name = registerDto.Name,
                    Surname = registerDto.Surname,
                    CreatedDate = DateTime.UtcNow,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = registerDto.Email,
                    IsActive = true
                };

                _studentService.Add(newStudent);

                var createToken = CreateToken(newStudent.Id, registerDto.UserType);

                if (createToken.Success == false)
                {
                    return new ErrorDataResult<bool>(false, createToken.Message);
                }

            }

            return new SuccessDataResult<bool>(true, "Ok");

        }

        private IDataResult<bool> CheckLecturerAndCreateToken(bool isLogin, LoginDto loginDto = null, RegisterDto registerDto = null, Lecturer lecturer = null, byte[] passwordSalt = null, byte[] passwordHash = null)
        {
            if (isLogin)
            {
                if (lecturer == null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip öğretmen bulunmamaktadır.");
                }

                var verify = HashingHelper.VerifyPasswordHash(loginDto.Password, lecturer.PasswordSalt, lecturer.PasswordHash);

                if (!verify)
                {
                    return new ErrorDataResult<bool>(false, "Şifreniz yanlış!");
                }

                var createToken = CreateToken(lecturer.Id, loginDto.UserType);

                if (createToken.Success == false)
                {
                    return new ErrorDataResult<bool>(false, createToken.Message);
                }

            }
            else
            {
                var checkLecturer = _userService.GetByFilter(x => x.Email == registerDto.Email);

                if (checkLecturer != null)
                {
                    return new ErrorDataResult<bool>(false, "Bu maile sahip bir öğrenci zaten var.");
                }


                Lecturer newLecturer = new Lecturer
                {
                    Name = registerDto.Name,
                    Surname = registerDto.Surname,
                    CreatedDate = DateTime.UtcNow,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = registerDto.Email,
                    IsActive = true
                };

                _lecturerService.Add(newLecturer);

                var createToken = CreateToken(newLecturer.Id, registerDto.UserType);

                if (createToken.Success == false)
                {
                    return new ErrorDataResult<bool>(false, createToken.Message);
                }
            }

            return new SuccessDataResult<bool>(true, "Ok");
        }


        public IDataResult<bool> Register(RegisterDto registerDto)
        {
            try
            {
                if (registerDto.Name == null || registerDto.Surname == null || registerDto.Email == null || registerDto.Password == null)
                {
                    return new ErrorDataResult<bool>(false, "Lütfen tüm bilgilerinizi eksiksiz doldurun.");
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


                if (registerDto.UserType < 0 || registerDto.UserType > 2)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı tipi bulunamadı");
                }

                IDataResult<bool> result;

                if (registerDto.UserType == (byte)UserTypeEnum.User)
                {
                    result = CheckUserAndCreateToken(false, null, registerDto, null, passwordSalt, passwordHash);
                }
                else if (registerDto.UserType == (byte)UserTypeEnum.Student)
                {
                    result = CheckStudentAndCreateToken(false, null, registerDto, null, passwordSalt, passwordHash);
                }
                else
                {
                    result = CheckLecturerAndCreateToken(false, null, registerDto, null, passwordSalt, passwordHash);
                }

                return result;
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }
    }
}
