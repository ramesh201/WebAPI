
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Common;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly DBSchoolContext dbSchoolContext;

        private readonly DBSchoolContext _db;
        SubjectManager sm;
        public SubjectController(DBSchoolContext DBContext)
        {
            this._db = DBContext;
            sm = new SubjectManager(DBContext);
        }

        [HttpGet("GetSubjects")]
        public List<SubjectListDTO> Get()
        {
            var result = sm.GetAllSubjects();
            return result;
        }

        [HttpPost("AddOrUpdateSubject")]
        public int AddOrUpdateSubject([FromBody] SubjectDTO subject)
        {
            var result = sm.AddOrUpdateSubject(subject);
            return result;
        }
        [HttpDelete("InactiveSubject/{id}")]
        public int InactiveSubject(int id)
        {
            var result = sm.InactiveSubject(id);
            return result;
        }
    }
}

