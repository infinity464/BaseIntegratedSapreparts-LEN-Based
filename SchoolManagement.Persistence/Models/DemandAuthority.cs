using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class DemandAuthority
    {
        public DemandAuthority()
        {
            Acceptances = new HashSet<Acceptance>();
            Demands = new HashSet<Demand>();
        }

        public int DemandAuthorityId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Acceptance> Acceptances { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
    }
}
