using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class PmsNumberEntry
    {
        public PmsNumberEntry()
        {
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPm>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
        }

        public int PmsNumberEntryId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? ItemDetailId { get; set; }
        public string PmsNo { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual EquipmentName EquipmentName { get; set; }
        public virtual ItemDetail ItemDetail { get; set; }
        public virtual ICollection<JobDescriptionEntryPm> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
    }
}
