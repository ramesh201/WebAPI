﻿
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    
    public partial class ContatctPersonEmail
    {
        [Key]
        public int EmailId { get; set; }
        public string EmailAddress { get; set; }
        public int ContatctPersonId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}

