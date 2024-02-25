using OgrenciBilgiSistemi.Dal.Concrete.Base;
using OgrenciBilgiSistemi.Dal.Interface;
using OgrenciBilgiSistemi.Entity.Context;
using OgrenciBilgiSistemi.Entity.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Dal.Concrete
{
    public class LecturerDal : EfRepositoryBase<Lecturer, ObsDbContext>, ILecturerDal
    {
    }
}
