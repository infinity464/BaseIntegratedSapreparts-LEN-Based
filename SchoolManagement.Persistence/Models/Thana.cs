using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class Thana
    {
        public int ThanaId { get; set; }
        public int DistrictId { get; set; }
        public string ThanaName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual District District { get; set; } = null!;
    }
}
