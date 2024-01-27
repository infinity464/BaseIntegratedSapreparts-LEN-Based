using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class ForceType
    {
        public ForceType()
        {
            BaseNames = new HashSet<BaseName>();
        }

        public int ForceTypeId { get; set; }
        public string ForceTypeName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<BaseName> BaseNames { get; set; }
    }
}
