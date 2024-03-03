using OgrenciBilgiSistemi.Core.Result;
using OgrenciBilgiSistemi.Entity.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Business.Interface
{
    public interface ILecturerService
    {
        IDataResult<Lecturer> GetByFilter(Expression<Func<Lecturer, bool>> filter);
        IDataResult<bool> Add(Lecturer lecturer);

        //IDataResult<bool> Update(UserRequestDto userRequestDto);
        //IDataResult<bool> Remove(int userId);
        //IDataResult<bool> MakePassive(int userId);
        //IDataResult<bool> MakeActive(int userId);
        //IDataResult<List<UserListDto>> GetList();
        //IDataResult<List<UserListDto>> GetActiveList();
        //IDataResult<List<UserListDto>> GetPassiveList();
        //IDataResult<UserListDto> GetById(int userId);
    }
}
