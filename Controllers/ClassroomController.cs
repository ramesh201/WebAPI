
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : ControllerBase
    {
        private readonly DBSchoolContext dbSchoolContext;
        ClassroomManager clsmanager;
        public ClassroomController(DBSchoolContext DBContext)
        {
            this.dbSchoolContext = DBContext;
            clsmanager = new ClassroomManager(DBContext);
        }

        [HttpGet("GetClassrooms")]
        public List<ClassroomListDTO> Get()
        {
            var result = clsmanager.GetAllClassrooms();
            return result;
        }

        [HttpPost("AddOrUpdateClassroom")]
        public int AddOrUpdateClassroom([FromBody] ClassroomDTO classroom)
        {
            var result = clsmanager.AddOrUpdateClassroom(classroom);
            return result;
        }
        [HttpDelete("InactiveClassroom/{id}")]
        public int InactiveClassroom(int id)
        {
            var result = clsmanager.InactiveClassroom(id);
            return result;
        }
    }
}

