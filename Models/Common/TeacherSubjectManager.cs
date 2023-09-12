
using WebAPI.Models.DTO;
using System.Data;

namespace WebAPI.Models.Common
{
    public class TeacherSubjectManager
    {
        public TeacherSubjectManager()
        {
        }

        private DBSchoolContext schoolContext;
        public TeacherSubjectManager(DBSchoolContext db)
        {
            schoolContext = db;
        }

        public List<SubjectAllocatedToTeacherDTO> GetAllTeacherSubjects(int teacherId)
        {
            var result = new List<SubjectAllocatedToTeacherDTO>();
            try
            {
                result = (from subj in schoolContext.SchoolSubject
                          join aloSub in schoolContext.AllocateSubject
                          on subj.SubjectId equals aloSub.SubjectId
                          join teacher in schoolContext.Teacher
                          on aloSub.TeacherId equals teacher.TeacherId
                          where teacher.TeacherId == teacherId && teacher.IsActive
                          && subj.IsActive && aloSub.IsActive
                          select new SubjectAllocatedToTeacherDTO()
                          {
                              AllocateSubjectId = aloSub.AllocateSubjectId,
                              SubjectId = subj.SubjectId,
                              SubjectName = subj.SubjectName,
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


        public int AddOrUpdateTeacherSubject(SubjectAllocatedToTeacherDTO teacherSubject)
        {
            var result = 0;

            try
            {
                var subjectExist = schoolContext.AllocateSubject.
                    Any(f => f.TeacherId == teacherSubject.TeacherId && f.SubjectId == teacherSubject.SubjectId && f.IsActive);

                if (!subjectExist)
                {
                    var record = schoolContext.AllocateSubject.
                        Where(f => f.AllocateSubjectId == teacherSubject.AllocateSubjectId).FirstOrDefault();
                    if (record == null)
                    {
                        var teacherSubjectObj = new AllocateSubject();
                        teacherSubjectObj.TeacherId = teacherSubject.TeacherId;
                        teacherSubjectObj.SubjectId = teacherSubject.SubjectId;
                        teacherSubjectObj.IsActive = true;
                        teacherSubjectObj.CreatedDate = DateTime.Now;
                        teacherSubjectObj.CreatedBy = -1;

                        schoolContext.AllocateSubject.Add(teacherSubjectObj);
                    }
                    result += schoolContext.SaveChanges();
                }
                else result = -1;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int InactiveTeacherSubject(int id)
        {
            var result = 0;
            try
            {
                var record = schoolContext.AllocateSubject
                    .Where(f => f.AllocateSubjectId == id).FirstOrDefault();
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


    }
}

