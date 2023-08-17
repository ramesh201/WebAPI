
using WebAPI.Models.DTO;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models.Common
{
    public class SubjectManager
    {
        public SubjectManager()
        {
        }

        private DBSchoolContext schoolContext;
        public SubjectManager(DBSchoolContext db)
        {
            schoolContext = db;
        }
        #region Operations

        public List<SubjectListDTO> GetAllSubjects()
        {
            var result = new List<SubjectListDTO>();

            try
            {
                result = schoolContext.SchoolSubject.Where(w => w.IsActive)
                    .Select(s => new SubjectListDTO()
                    {
                        SubjectId = s.SubjectId,
                        SubjectName = s.SubjectName,

                    }).ToList();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public int AddOrUpdateSubject(SubjectDTO subject)
        {
            var result = 0;

            try
            {
                var record = schoolContext.SchoolSubject.
                    Where(f => f.SubjectId == subject.SubjectId).FirstOrDefault();
                if (record == null)
                {
                    var subjectObj = new SchoolSubject();
                    subjectObj.SubjectName = subject.SubjectName;
                    subjectObj.IsActive = true;
                    subjectObj.CreatedDate = DateTime.Now;
                    subjectObj.CreatedBy = -1;

                    schoolContext.SchoolSubject.Add(subjectObj);
                }
                else
                {
                    record.SubjectName = subject.SubjectName;
                    record.ModifiedDate = DateTime.Now;
                    record.ModifiedBy = -1;
                    schoolContext.Entry(record).State = EntityState.Modified;

                }

                result += schoolContext.SaveChanges();

            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        public int InactiveSubject(int id)
        {
            var result = 0;
            try
            {
                var record = schoolContext.SchoolSubject
                    .Where(f => f.SubjectId == id).FirstOrDefault();
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

