
namespace WebAPI.Models
{
    public partial class Classroom
    {
        public int ClassroomId { get; set; }
        public string ClassroomName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

    }
}

