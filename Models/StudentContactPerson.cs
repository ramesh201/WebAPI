
namespace WebAPI.Models
{
    public partial class StudentContactPerson
    {
        public int StudentContactPersonId { get; set; }
        public string StudentContactPersonName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}

