using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class Group
    {
        public Group()
        {
            Demands = new HashSet<Demand>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Demand> Demands { get; set; }
    }
}
