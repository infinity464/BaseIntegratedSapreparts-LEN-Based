using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public string? ShortName { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
