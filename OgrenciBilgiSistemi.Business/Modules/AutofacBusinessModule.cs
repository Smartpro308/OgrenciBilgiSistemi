using Autofac;
using OgrenciBilgiSistemi.Business.Concrete;
using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Core.Security.Helpers;
using OgrenciBilgiSistemi.Dal.Concrete;
using OgrenciBilgiSistemi.Dal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Business.Modules
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Dal
            builder.RegisterType<UserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<CourseDal>().As<ICourseDal>().SingleInstance();
            builder.RegisterType<LecturerDal>().As<ILecturerDal>().SingleInstance();
            builder.RegisterType<StudentDal>().As<IStudentDal>().SingleInstance();
            builder.RegisterType<LecturerCourseDal>().As<ILecturerCourseDal>().SingleInstance();
            builder.RegisterType<StudentCourseDal>().As<IStudentCourseDal>().SingleInstance();
            builder.RegisterType<UserSessionDal>().As<IUserSessionDal>().SingleInstance();
            builder.RegisterType<TokenHelper>().As<ITokenHelper>().SingleInstance();

            //Service
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
            builder.RegisterType<StudentService>().As<IStudentService>().SingleInstance();
            builder.RegisterType<LecturerService>().As<ILecturerService>().SingleInstance();


        }
    }
}
