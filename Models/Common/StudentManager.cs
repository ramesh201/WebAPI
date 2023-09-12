
using WebAPI.Models.DTO;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.Common
{
    public class StudentManager
    {
        public StudentManager()
        {
        }

        private DBSchoolContext schoolContext;
        public StudentManager(DBSchoolContext db)
        {
            schoolContext = db;
        }
        #region  Operations

        public List<StudentListDTO> GetAllStudents()
        {

            var result = new List<StudentListDTO>();

            try
            {

                result = (from st in schoolContext.StudentTbl
                          join cls in schoolContext.Classroom

                          on st.ClassroomId equals cls.ClassroomId
                          join shcp in schoolContext.StudentHasContactPerson
                          on st.StudentId equals shcp.StudentId
                          join cp in schoolContext.StudentContactPerson
                          on shcp.ContactPersonId equals cp.StudentContactPersonId
                          join cpPhone in schoolContext.ContactPersonMobile
                          on shcp.ContactPersonId equals cpPhone.ContatctPersonId
                          join cpEmail in schoolContext.ContatctPersonEmail
                          on cp.StudentContactPersonId equals cpEmail.ContatctPersonId
                          where st.IsActive
                          select new StudentListDTO
                          {
                              StudentId = st.StudentId,
                              StudentFirstName = st.StudentFirstName,
                              StudentLastName = st.StudentLastName,
                              Classroom = cls.ClassroomName,
                              ContactPersonContactNo = cpPhone.MobileNumber,
                              ContactPersonEmail = cpEmail.EmailAddress,
                              DateOfBirth = st.DoB,
                              ContactPersonName = cp.StudentContactPersonName,

                          }).ToList();
            }
            catch (Exception)
            {

            }

            return result;
        }

        public int AddOrUpdateStudent(StudentDTO student)
        {
            var result = 0;

            try
            {
                var studentRecord = schoolContext.StudentTbl.FirstOrDefault(f => f.StudentId == student.StudentId);
                if (studentRecord == null)
                {
                    var studentObj = new Student();
                    studentObj.StudentFirstName = student.StudentFirstName;
                    studentObj.StudentLastName = student.StudentLastName;
                    studentObj.ClassroomId = int.Parse(student.ClassroomId);
                    var dob = new DateTime();
                    DateTime.TryParse(student.DateOfBirth, out dob);
                    studentObj.DoB = dob;
                    studentObj.IsActive = true;
                    studentObj.CreatedDate = DateTime.Now;
                    studentObj.CreatedBy = -1;

                    schoolContext.StudentTbl.Add(studentObj);

                    var studentContactPerson = new StudentContactPerson();
                    studentContactPerson.StudentContactPersonName = student.ContactPersonName;
                    studentContactPerson.IsActive = true;
                    studentContactPerson.CreatedDate = DateTime.Now;
                    studentContactPerson.CreatedBy = -1;

                    schoolContext.StudentContactPerson.Add(studentContactPerson);
                    result = schoolContext.SaveChanges();

                    var studentHasContactPerson = new StudentHasContactPerson();
                    studentHasContactPerson.StudentId = studentObj.StudentId;
                    studentHasContactPerson.ContactPersonId = studentContactPerson.StudentContactPersonId;

                    schoolContext.StudentHasContactPerson.Add(studentHasContactPerson);

                    var contactPersonEmail = new ContatctPersonEmail();
                    contactPersonEmail.EmailAddress = student.ContactPersonEmail;
                    contactPersonEmail.ContatctPersonId = studentContactPerson.StudentContactPersonId;
                    contactPersonEmail.IsActive = true;
                    contactPersonEmail.CreatedDate = DateTime.Now;
                    contactPersonEmail.CreatedBy = -1;

                    schoolContext.ContatctPersonEmail.Add(contactPersonEmail);

                    var contactPersonMobile = new ContactPersonMobile();
                    contactPersonMobile.MobileNumber = student.ContactPersonContactNo;
                    contactPersonMobile.ContatctPersonId = studentContactPerson.StudentContactPersonId;
                    contactPersonMobile.IsActive = true;
                    contactPersonMobile.CreatedDate = DateTime.Now;
                    contactPersonMobile.CreatedBy = -1;

                    schoolContext.ContactPersonMobile.Add(contactPersonMobile);

                }
                else
                {
                    studentRecord.StudentFirstName = student.StudentFirstName;
                    studentRecord.StudentLastName = student.StudentLastName;
                    studentRecord.ClassroomId = int.Parse(student.ClassroomId);
                    var dob = new DateTime();
                    DateTime.TryParse(student.DateOfBirth, out dob);
                    studentRecord.DoB = dob;
                    studentRecord.ModifiedDate = DateTime.Now;
                    studentRecord.ModifiedBy = -1;

                    schoolContext.Entry(studentRecord).State = EntityState.Modified;

                    var studentHasContactPerson = schoolContext.StudentHasContactPerson
                        .FirstOrDefault(f => f.StudentId == student.StudentId);

                    var studentContactPerson = schoolContext.StudentContactPerson
                        .Where(f => f.StudentContactPersonId == studentHasContactPerson.ContactPersonId).FirstOrDefault();
                    studentContactPerson.StudentContactPersonName = student.ContactPersonName;
                    studentContactPerson.ModifiedDate = DateTime.Now;
                    studentContactPerson.ModifiedBy = -1;

                    schoolContext.Entry(studentContactPerson).State = EntityState.Modified;

                    var contactPersonEmail = schoolContext.ContatctPersonEmail
                        .FirstOrDefault(f => f.ContatctPersonId == studentContactPerson.StudentContactPersonId);
                    contactPersonEmail.EmailAddress = student.ContactPersonEmail;
                    contactPersonEmail.ModifiedDate = DateTime.Now;
                    contactPersonEmail.ModifiedBy = -1;

                    schoolContext.Entry(contactPersonEmail).State = EntityState.Modified;

                    var contactPersonMobile = schoolContext.ContactPersonMobile
                        .FirstOrDefault(f => f.ContatctPersonId == studentContactPerson.StudentContactPersonId);
                    contactPersonMobile.MobileNumber = student.ContactPersonContactNo;
                    contactPersonMobile.ModifiedDate = DateTime.Now;
                    contactPersonMobile.ModifiedBy = -1;

                    schoolContext.Entry(contactPersonMobile).State = EntityState.Modified;
                }

                result += schoolContext.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }


            return result;
        }

        public int InactiveStudent(int id)
        {
            var result = 0;
            try
            {
                var studentRecord = schoolContext.StudentTbl.Where(f => f.StudentId == id).FirstOrDefault();
                studentRecord.IsActive = false;
                studentRecord.ModifiedDate = DateTime.Now;
                studentRecord.ModifiedBy = -1;

                result = schoolContext.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public StudentDetailsReportDTO GetStudentDetailsReport(int studentId)
        {
            var result = new StudentDetailsReportDTO();
            try
            {
                var query1 = (from st in schoolContext.StudentTbl
                              join cls in schoolContext.Classroom
                              on st.ClassroomId equals cls.ClassroomId
                              join shcp in schoolContext.StudentHasContactPerson
                              on st.StudentId equals shcp.StudentId
                              join cp in schoolContext.StudentContactPerson
                              on shcp.ContactPersonId equals cp.StudentContactPersonId
                              join cpPhone in schoolContext.ContactPersonMobile
                              on shcp.ContactPersonId equals cpPhone.ContatctPersonId
                              join cpEmail in schoolContext.ContatctPersonEmail
                              on cp.StudentContactPersonId equals cpEmail.ContatctPersonId
                              where st.IsActive && st.StudentId == studentId
                              select new StudentDetailsDTO
                              {
                                  StudentId = st.StudentId,
                                  StudentFirstName = st.StudentFirstName,
                                  StudentLastName = st.StudentLastName,
                                  ClassroomId = cls.ClassroomId,
                                  ClassroomName = cls.ClassroomName,
                                  ContactPersonMobile = cpPhone.MobileNumber,
                                  ContactPersonEmail = cpEmail.EmailAddress,
                                  DateOfBirth = st.DoB,
                                  ContactPersonName = cp.StudentContactPersonName,

                              });

                var query2 = (from aloCls in schoolContext.AllocateClassroom
                              join aloSub in schoolContext.AllocateSubject
                              on aloCls.TeacherId equals aloSub.TeacherId
                              join sub in schoolContext.SchoolSubject
                              on aloSub.SubjectId equals sub.SubjectId
                              join teacher in schoolContext.Teacher
                              on aloSub.TeacherId equals teacher.TeacherId
                              where aloCls.ClassroomId == query1.FirstOrDefault().ClassroomId
                              && aloSub.IsActive && aloCls.IsActive
                              select new SubjectAllocatedToTeacherDTO()
                              {
                                  ClassroomId = aloCls.ClassroomId,
                                  SubjectName = sub.SubjectName,
                                  TeacherName = teacher.TeacherFirstName + " " + teacher.TeacherLastName,
                              }
                              ).ToList();

                result = new StudentDetailsReportDTO(query1.FirstOrDefault(), query2);

            }
            catch (Exception)
            {

            }

            return result;
        }

        #endregion
    }
}

