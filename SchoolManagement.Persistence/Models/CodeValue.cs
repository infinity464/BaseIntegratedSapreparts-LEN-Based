using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class CodeValue
    {
        public int CodeValueId { get; set; }
        public int CodeValueTypeId { get; set; }
        public string Code { get; set; } = null!;
        public string? TypeValue { get; set; }
        public string? AdditonalValue { get; set; }
        public string? DisplayCode { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CodeValueType CodeValueType { get; set; } = null!;
    }
}
