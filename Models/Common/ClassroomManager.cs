
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.DTO;

namespace WebAPI.Models.Common
{
    public class ClassroomManager
    {
        public ClassroomManager()
        {
        }

        private DBSchoolContext schoolContext;
        public ClassroomManager(DBSchoolContext db)
        {
            schoolContext = db;
        }

        #region Classrooms Operations

        public List<ClassroomListDTO> GetAllClassrooms()
        {
            var result = new List<ClassroomListDTO>();

            try
            {
                result = (from cls in schoolContext.Classroom
                          where cls.IsActive
                          orderby cls.ClassroomName ascending
                          select new ClassroomListDTO
                          {
                              ClassroomId = cls.ClassroomId,
                              ClassroomName = cls.ClassroomName,
                          }).ToList();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public int AddOrUpdateClassroom(ClassroomDTO classroom)
        {
            var res = 0;
            var result = new ClassroomListDTO();

            try
            {
                var record = schoolContext.Classroom.Where(w => w.ClassroomId == classroom.ClassroomId).FirstOrDefault();
                if (record == null)
                {
                    var classObj = new Classroom();
                    classObj.ClassroomName = classroom.ClassroomName;
                    classObj.IsActive = true;
                    classObj.CreatedDate = DateTime.Now;
                    classObj.CreatedBy = -1;

                    schoolContext.Classroom.Add(classObj);
                }
                else
                {
                    record.ClassroomName = classroom.ClassroomName;
                    record.ModifiedDate = DateTime.Now;
                    record.ModifiedBy = -1;
                    schoolContext.Entry(record).State = EntityState.Modified;
                }

                res += schoolContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw;
            }

            return res;

        }

        public int InactiveClassroom(int id)
        {
            var result = 0;

            try
            {
                var record = schoolContext.Classroom.Where(f => f.ClassroomId == id).FirstOrDefault();
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

