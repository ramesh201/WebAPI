
namespace WebAPI.Models.DTO
{

    public class SubjectAllocatedToTeacherDTO
    {
        public int AllocateSubjectId { get; set; }
        public int ClassroomId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
   
}

