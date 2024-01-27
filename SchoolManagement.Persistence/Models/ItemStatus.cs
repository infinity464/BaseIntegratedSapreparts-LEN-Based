using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class ItemStatus
    {
        public ItemStatus()
        {
            Demands = new HashSet<Demand>();
            SurveyItems = new HashSet<SurveyItem>();
        }

        public int ItemStatusId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
    }
}
