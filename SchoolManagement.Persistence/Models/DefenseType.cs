using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class DefenseType
    {
        public int DefenseTypeId { get; set; }
        public string DefenseTypeName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
