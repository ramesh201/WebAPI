
namespace WebAPI.Models.DTO
{

    public class ClassroomAllocatedToTeacherDTO
    {
        public int AllocateClassroomId { get; set; }
        public int ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
   
}

