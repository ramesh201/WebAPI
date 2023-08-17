
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherClassroomController : ControllerBase
    {
        private readonly DBSchoolContext dbSchoolContext;

        private readonly DBSchoolContext _db;
        TeacherClassroomManager tcm;
        public TeacherClassroomController(DBSchoolContext DBContext)
        {
            this._db = DBContext;
            tcm = new TeacherClassroomManager(DBContext);
        }

        [HttpGet("GetAllTeacherClassrooms/{id}")]
        public List<ClassroomAllocatedToTeacherDTO> GetAllTeacherClassrooms(int id)
        {
            var result = tcm.GetAllTeacherClassrooms(id);
            return result;
        }
      
        [HttpPost("AddOrUpdateTeacherClassroom")]
        public int AddOrUpdateTeacherClassroom([FromBody] ClassroomAllocatedToTeacherDTO teacherClassroom)
        {
            var result = tcm.AddOrUpdateTeacherClassroom(teacherClassroom);
            return result;
        }
        [HttpDelete("InactiveTeacherClassroom/{id}")]
        public int InactiveTeacherClassroom(int id)
        {
            var result = tcm.InactiveTeacherClassroom(id);
            return result;
        }
    }
}

