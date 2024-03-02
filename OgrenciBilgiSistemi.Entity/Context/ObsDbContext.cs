using OgrenciBilgiSistemi.Entity.Entitites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Entity.Context
{
    public class ObsDbContext : DbContext
    {
        public ObsDbContext()
        {
            Database.Connection.ConnectionString = @"Server=DESKTOP-GE22EA4;Database=ObsDb;User Id=sa;Password=1234;";
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<LecturerCourse> LecturerCourses { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
    }
}
