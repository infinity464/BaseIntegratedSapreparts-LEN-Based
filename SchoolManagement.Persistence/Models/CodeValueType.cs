using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class CodeValueType
    {
        public CodeValueType()
        {
            CodeValues = new HashSet<CodeValue>();
        }

        public int CodeValueTypeId { get; set; }
        public string Type { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CodeValue> CodeValues { get; set; }
    }
}
