
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    
    public partial class AllocateClassroom
    {
        [Key]
        public int AllocateClassroomId { get; set; }
        public int ClassroomId { get; set; }
        public int TeacherId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}

