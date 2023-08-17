using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly DBSchoolContext dbSchoolContext;

        private readonly DBSchoolContext _db;
        TeacherSubjectManager tsm;
        public TeacherSubjectController(DBSchoolContext DBContext)
        {
            this._db = DBContext;
            tsm = new TeacherSubjectManager(DBContext);
        }

        [HttpGet("GetAllTeacherSubjects/{id}")]
        public List<SubjectAllocatedToTeacherDTO> GetAllTeacherSubjects(int id)
        {
            var result = tsm.GetAllTeacherSubjects(id);
            return result;
        }
      
        [HttpPost("AddOrUpdateTeacherSubject")]
        public int AddOrUpdateTeacherSubject([FromBody] SubjectAllocatedToTeacherDTO teacherSubject)
        {
            var result = tsm.AddOrUpdateTeacherSubject(teacherSubject);
            return result;
        }
        [HttpDelete("InactiveTeacherSubject/{id}")]
        public int InactiveTeacherSubject(int id)
        {
            var result = tsm.InactiveTeacherSubject(id);
            return result;
        }
    }
}

