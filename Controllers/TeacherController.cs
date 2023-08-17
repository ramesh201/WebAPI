
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly DBSchoolContext dbSchoolContext;
        
        private readonly DBSchoolContext _db;
        TeacherManager tm;
        public TeacherController(DBSchoolContext DBContext)
        {
            this._db = DBContext;
            tm = new TeacherManager(DBContext);
        }

        [HttpGet("GetTeachers")]
        public List<TeacherListDTO> Get()
        {
            var result = tm.GetAllTeachers();
            return result;
        }
        
        [HttpPost("AddOrUpdateTeacher")]
        public int AddOrUpdateTeacher([FromBody] TeacherDTO teacher)
        {
            var result = tm.AddOrUpdateTeacher(teacher);
            return result;
        }
        [HttpDelete("InactiveTeacher/{id}")]
        public int InactiveTeacher(int id)
        {
            var result = tm.InactiveTeacher(id);
            return result;
        }
    }
}

