
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    
    public partial class SchoolSubject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}

