
using WebAPI.Models.DTO;
using System.Data;

namespace WebAPI.Models.Common
{
    public class TeacherClassroomManager
    {
        public TeacherClassroomManager()
        {
        }

        private DBSchoolContext schoolContext;
        public TeacherClassroomManager(DBSchoolContext db)
        {
            schoolContext = db;
        }

        public List<ClassroomAllocatedToTeacherDTO> GetAllTeacherClassrooms(int teacherId)
        {
            var result = new List<ClassroomAllocatedToTeacherDTO>();
            try
            {
                result = (from cls in schoolContext.Classroom
                          join aloCls in schoolContext.AllocateClassroom
                          on cls.ClassroomId equals aloCls.ClassroomId
                          join teacher in schoolContext.Teacher
                          on aloCls.TeacherId equals teacher.TeacherId
                          where teacher.TeacherId == teacherId && teacher.IsActive
                          && cls.IsActive && aloCls.IsActive
                          select new ClassroomAllocatedToTeacherDTO()
                          {
                              AllocateClassroomId = aloCls.AllocateClassroomId,
                              ClassroomId = cls.ClassroomId,
                              ClassroomName = cls.ClassroomName,
                              TeacherId = teacher.TeacherId,
                              TeacherName = teacher.TeacherFirstName + " " + teacher.TeacherLastName
                          }
                          ).ToList();

            }
            catch (Exception ex)
            {

            }

            return result;
        }


        public int AddOrUpdateTeacherClassroom(ClassroomAllocatedToTeacherDTO teacherClassroom)
        {
            var result = 0;

            try
            {
                var record = schoolContext.AllocateClassroom
                    .Where(f => f.AllocateClassroomId == teacherClassroom.AllocateClassroomId).FirstOrDefault();
                if (record == null)
                {
                    var teacherClassroomObj = new AllocateClassroom();
                    teacherClassroomObj.TeacherId = teacherClassroom.TeacherId;
                    teacherClassroomObj.ClassroomId = teacherClassroom.ClassroomId;
                    teacherClassroomObj.IsActive = true;
                    teacherClassroomObj.CreatedDate = DateTime.Now;
                    teacherClassroomObj.CreatedBy = -1;

                    schoolContext.AllocateClassroom.Add(teacherClassroomObj);
                }

                result += schoolContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        public int InactiveTeacherClassroom(int id)
        {
            var result = 0;
            try
            {
                var record = schoolContext.AllocateClassroom
                    .Where(f => f.AllocateClassroomId == id).FirstOrDefault();
                record.IsActive = false;
                record.ModifiedDate = DateTime.Now;
                record.ModifiedBy = -1;

                result = schoolContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }


    }
}

