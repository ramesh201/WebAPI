
using WebAPI.Models.DTO;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.Common
{
    public class TeacherManager
    {
        public TeacherManager()
        {
        }

        private DBSchoolContext schoolContext;
        public TeacherManager(DBSchoolContext db)
        {
            schoolContext = db;
        }

        #region Operations

        public List<TeacherListDTO> GetAllTeachers()
        {

            var result = new List<TeacherListDTO>();

            try
            {
                result = schoolContext.Teacher
                    .Where(w => w.IsActive).Select(s => new TeacherListDTO()
                    {
                        TeacherId = s.TeacherId,
                        TeacherFirstName = s.TeacherFirstName,
                        TeacherLastName = s.TeacherLastName,
                        TeacherEmail = s.TeacherEmail,
                        TeacherContatctNo = s.TeacherContactNo

                    }).ToList();

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public int AddOrUpdateTeacher(TeacherDTO teacher)
        {
            var result = 0;

            try
            {
                var record = schoolContext.Teacher
                    .Where(f => f.TeacherId == teacher.TeacherId).FirstOrDefault();
                if (record == null)
                {
                    var teacherObj = new Teacher();
                    teacherObj.TeacherFirstName = teacher.TeacherFirstName;
                    teacherObj.TeacherLastName = teacher.TeacherLastName;
                    teacherObj.TeacherEmail = teacher.TeacherEmail;
                    teacherObj.TeacherContactNo = teacher.TeacherContatctNo;
                    teacherObj.IsActive = true;
                    teacherObj.CreatedDate = DateTime.Now;
                    teacherObj.CreatedBy = -1;

                    schoolContext.Teacher.Add(teacherObj);
                }
                else
                {
                    record.TeacherFirstName = teacher.TeacherFirstName;
                    record.TeacherLastName = teacher.TeacherLastName;
                    record.TeacherEmail = teacher.TeacherEmail;
                    record.TeacherContactNo = teacher.TeacherContatctNo;
                    record.ModifiedDate = DateTime.Now;
                    record.ModifiedBy = -1;
                    schoolContext.Entry(record).State = EntityState.Modified;

                }

                result += schoolContext.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int InactiveTeacher(int id)
        {
            var result = 0;
            try
            {
                var record = schoolContext.Teacher
                    .Where(f => f.TeacherId == id).FirstOrDefault();
                record.IsActive = false;
                record.ModifiedDate = DateTime.Now;
                record.ModifiedBy = -1;

                result = schoolContext.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        #endregion
    }
}

