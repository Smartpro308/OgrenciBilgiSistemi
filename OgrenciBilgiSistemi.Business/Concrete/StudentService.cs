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
    public class StudentService : IStudentService
    {
        private readonly IStudentDal _studentDal;

        public StudentService(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        public IDataResult<bool> Add(Student student)
        {
            try
            {
                if (student == null)
                {
                    return new ErrorDataResult<bool>(false, "Öğrenci bilgilerini kontrol edin!");
                }
                _studentDal.Add(student);
                return new SuccessDataResult<bool>(true);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message);

            }
        }

        public IDataResult<Student> GetByFilter(Expression<Func<Student, bool>> filter)
        {
            try
            {
                var filteredStudent = _studentDal.GetByFilter(filter);

                if (filteredStudent == null)
                {
                    return new ErrorDataResult<Student>(null, "Kullanıcı bulunamadı!");
                }

                return new SuccessDataResult<Student>(filteredStudent, "Ok");
            }
            catch (Exception e)
            {
                return new ErrorDataResult<Student>(null, e.Message);
            }
        }
    }
}
