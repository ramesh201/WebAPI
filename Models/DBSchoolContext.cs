
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class DBSchoolContext : DbContext
    {
        public DBSchoolContext(DbContextOptions<DBSchoolContext> options) : base(options) { }

        public DBSchoolContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        public DbSet<Student> StudentTbl { get; set; }
        public DbSet<StudentContactPerson> StudentContactPerson { get; set; }
        public DbSet<StudentHasContactPerson> StudentHasContactPerson { get; set; }
        public DbSet<ContactPersonMobile> ContactPersonMobile { get; set; }
        public DbSet<ContatctPersonEmail> ContatctPersonEmail { get; set; }
        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<SchoolSubject> SchoolSubject { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<AllocateClassroom> AllocateClassroom { get; set; }
        public DbSet<AllocateSubject> AllocateSubject { get; set; }
    }
}
