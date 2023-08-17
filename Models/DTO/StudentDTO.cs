
namespace WebAPI.Models.DTO
{

    public class StudentListDTO
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonContactNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Classroom { get; set; }
    }
    public class StudentDTO
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonContactNo { get; set; }
        public string DateOfBirth { get; set; }
        public string ClassroomId { get; set; }
    }
    public class StudentDetailsDTO
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonMobile { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassroomId { get; set; }
        public string ClassroomName { get; set; }
    }
    public class StudentDetailsReportDTO
    {
        public StudentDetailsReportDTO()
        {
        }
        public StudentDetailsReportDTO(StudentDetailsDTO _studentDetails, List<SubjectAllocatedToTeacherDTO> _teacherSubjects)
        {
            StudentDetails = _studentDetails;
            TeacherSubjects = _teacherSubjects;
        }
        public StudentDetailsDTO StudentDetails { get; set; }
        public List<SubjectAllocatedToTeacherDTO> TeacherSubjects { get; set; }
    }
}

