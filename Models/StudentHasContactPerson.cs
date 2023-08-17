
namespace WebAPI.Models
{
    public partial class StudentHasContactPerson
    {
        public int StudentHasContactPersonId { get; set; }
        public int StudentId { get; set; }
        public int ContactPersonId { get; set; }
    }
}

