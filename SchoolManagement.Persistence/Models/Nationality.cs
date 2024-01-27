using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class Nationality
    {
        public int NationalityId { get; set; }
        public string NationalityName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
