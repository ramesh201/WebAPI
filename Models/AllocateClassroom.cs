
namespace WebAPI.Models
{
    public partial class AllocateSubject
    {
        public int AllocateSubjectId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}

