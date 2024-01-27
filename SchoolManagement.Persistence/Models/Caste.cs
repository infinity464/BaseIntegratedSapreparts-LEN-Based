using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class Caste
    {
        public int CasteId { get; set; }
        public int ReligionId { get; set; }
        public string CastName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Religion Religion { get; set; } = null!;
    }
}
