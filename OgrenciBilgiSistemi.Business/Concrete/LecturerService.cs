using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Core.Result;
using OgrenciBilgiSistemi.Dal.Concrete;
using OgrenciBilgiSistemi.Dal.Interface;
using OgrenciBilgiSistemi.Entity.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Business.Concrete
{
    public class LecturerService : ILecturerService
    {
        private readonly ILecturerDal _lecturerDal;

        public LecturerService(ILecturerDal lecturerDal)
        {
            _lecturerDal = lecturerDal;
        }

        public IDataResult<bool> Add(Lecturer lecturer)
        {
            try
            {
                if (lecturer == null)
                {
                    return new ErrorDataResult<bool>(false, "Öğretmen bilgilerini kontrol edin!");
                }
                _lecturerDal.Add(lecturer);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);

            }
        }

        public IDataResult<Lecturer> GetByFilter(Expression<Func<Lecturer, bool>> filter)
        {
            try
            {
                var filteredLecturer = _lecturerDal.GetByFilter(filter);

                if (filteredLecturer == null)
                {
                    return new ErrorDataResult<Lecturer>(null, "Öğretmen bulunamadı!");
                }

                return new SuccessDataResult<Lecturer>(filteredLecturer, "Ok");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<Lecturer>(null, e.Message);
            }
        }
    }
}
