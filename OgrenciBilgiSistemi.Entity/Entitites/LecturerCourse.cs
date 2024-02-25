using OgrenciBilgiSistemi.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Entity.Entitites
{
    public class LecturerCourse : IEntity
    {
        public int Id { get; set; }
        public int LecturerId { get; set; }
        public int CourseId { get; set; }
    }
}
