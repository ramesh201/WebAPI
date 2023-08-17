
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly DBSchoolContext _db;
        StudentManager sm;
        public StudentController(DBSchoolContext DBContext)
        {
            this._db = DBContext;
            sm = new StudentManager(DBContext);
        }

        [HttpGet("GetStudents")]
        public List<StudentListDTO> Get()
        {
            var result = sm.GetAllStudents();
            return result;
        }
        [HttpGet("GetStudentDetailsReport/{id}")]
        public StudentDetailsReportDTO GetStudentDetailsReport(int id)
        {
            var result = sm.GetStudentDetailsReport(id);
            return result;
        }


        [HttpPost("AddOrUpdateStudent")]
        public int AddOrUpdateStudent([FromBody] StudentDTO student)
        {
            var result = sm.AddOrUpdateStudent(student);
            return result;
        }
        [HttpDelete("InactiveStudent/{id}")]
        public int InactiveStudent(int id)
        {
            var result = sm.InactiveStudent(id);
            return result;
        }
    }
}

