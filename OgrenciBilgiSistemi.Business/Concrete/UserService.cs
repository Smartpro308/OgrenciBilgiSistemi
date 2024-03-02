using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Core.Result;
using OgrenciBilgiSistemi.Dal.Interface;
using OgrenciBilgiSistemi.Entity.Base;
using OgrenciBilgiSistemi.Entity.Dtos.User.Request;
using OgrenciBilgiSistemi.Entity.Entitites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        //Araştırma ödevleri. Haftaya sözlü yapılacak. Notlandırılacak.:

        //Acces Token nedir?
        //Autofac nedir?
        //API nedir?
        //Json ve Xml veri formatları nedir?

        //Kod ödevi:
        //User dışında kalan entityler için business katmanında servisler oluşturulup her birinde:
        //Add, Update, MakePassive, MakeActive GetList, GetById
        //Not : Her bir student ve her bir lecturer aslında birer user.


        public IDataResult<List<UserListDto>> GetActiveList()
        {
            try
            {
                var activeUserList = _userDal.GetList(x => x.IsActive == true).ToList();

                if (activeUserList.Count == 0)
                {
                    return new ErrorDataResult<List<UserListDto>>(null, "Aktif kullanıcı listesi bulunamadı!");
                }

                List<UserListDto> activeUserListDto = new List<UserListDto>();

                foreach (var user in activeUserList)
                {
                    activeUserListDto.Add(new UserListDto
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        CreatedDate = user.CreatedDate,
                        UpdatedDate = user.UpdatedDate,
                        Email = user.Email,
                        Id = user.Id,
                        IsActive = user.IsActive
                    });
                }

                return new SuccessDataResult<List<UserListDto>>(activeUserListDto);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<UserListDto>>(null, e.Message);
            }
        }

        public IDataResult<UserListDto> GetById(int userId)
        {
            try
            {
                var user = _userDal.GetByFilter(x => x.Id == userId);

                if (user == null)
                {
                    return new ErrorDataResult<UserListDto>(null, "Kullanıcı bulunamadı");
                }

                UserListDto userListDto = new UserListDto
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    CreatedDate = user.CreatedDate,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Id = user.Id,
                    UpdatedDate = user.UpdatedDate,
                };

                return new SuccessDataResult<UserListDto>(userListDto, "Ok");

            }
            catch (Exception e)
            {
                return new ErrorDataResult<UserListDto>(null, e.Message);
            }
        }

        public IDataResult<List<UserListDto>> GetList()
        {
            try
            {
                var userList = _userDal.GetList();

                if (userList.Count == 0)
                {
                    return new ErrorDataResult<List<UserListDto>>(null, "Kullanıcı listesi bulunamadı!");
                }

                List<UserListDto> userListDto = new List<UserListDto>();

                foreach (var user in userList)
                {
                    userListDto.Add(new UserListDto()
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        CreatedDate = user.CreatedDate,
                        UpdatedDate = user.UpdatedDate,
                        Email = user.Email,
                        Id = user.Id,
                        IsActive = user.IsActive
                    });
                }

                return new SuccessDataResult<List<UserListDto>>(userListDto);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<UserListDto>>(null, e.Message);
            }
        }

        public IDataResult<List<UserListDto>> GetPassiveList()
        {
            try
            {
                var userList = _userDal.GetList(x => x.IsActive == false).ToList();

                if (userList.Count == 0)
                {
                    return new ErrorDataResult<List<UserListDto>>(null, "Kullanıcı listesi bulunamadı!");
                }

                List<UserListDto> userListDto = new List<UserListDto>();

                foreach (var user in userList)
                {
                    userListDto.Add(new UserListDto()
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        CreatedDate = user.CreatedDate,
                        UpdatedDate = user.UpdatedDate,
                        Email = user.Email,
                        Id = user.Id,
                        IsActive = user.IsActive
                    });
                }

                return new SuccessDataResult<List<UserListDto>>(userListDto);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<UserListDto>>(null, e.Message);
            }
        }

        public IDataResult<bool> Remove(int userId)
        {
            try
            {
                var user = _userDal.GetByFilter(x => x.Id == userId);

                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı bulunamadı");
                }

                user.IsActive = false;

                _userDal.Update(user);

                return new SuccessDataResult<bool>(true, "Ok");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }

        public IDataResult<bool> Update(UserRequestDto userRequestDto)
        {
            try
            {
                var user = _userDal.GetByFilter(x => x.Id == userRequestDto.Id);

                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı bulunamadı!");
                }

                user.Name = userRequestDto.Name;
                user.Surname = userRequestDto.Surname;
                user.Email = userRequestDto.Email;

                _userDal.Update(user);

                return new SuccessDataResult<bool>(true, "Ok");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }

        public IDataResult<bool> MakePassive(int userId)
        {
            try
            {
                var user = _userDal.GetByFilter(x => x.Id == userId);

                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı bulunamadı!");
                }

                user.IsActive = false;

                _userDal.Update(user);

                return new SuccessDataResult<bool>(true, "Kullanıcı pasif edildi!");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }

        public IDataResult<bool> MakeActive(int userId)
        {
            try
            {
                var user = _userDal.GetByFilter(x => x.Id == userId);

                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı bulunamadı!");
                }

                user.IsActive = true;

                _userDal.Update(user);

                return new SuccessDataResult<bool>(true, "Kullanıcı pasif edildi!");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);
            }
        }

        public IDataResult<bool> Add(User user)
        {
            try
            {
                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı bilgilerini kontrol edin!");
                }
                _userDal.Add(user);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);

            }
        }

        public IDataResult<User> GetByFilter(Expression<Func<User, bool>> filter)
        {
            try
            {
                var filteredUser = _userDal.GetByFilter(filter);

                if (filteredUser == null)
                {
                    return new ErrorDataResult<User>(null, "Kullanıcı bulunamadı!");
                }
               
                return new SuccessDataResult<User>(filteredUser, "Ok");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<User>(null, e.Message);
            }
        }
    }
}
