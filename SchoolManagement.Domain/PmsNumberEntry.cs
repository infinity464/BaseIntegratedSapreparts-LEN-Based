using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public partial class PmsNumberEntry : BaseDomainEntity
    {
        public PmsNumberEntry()
        {
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPms>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();

        }

        public int PmsNumberEntryId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? ItemDetailId { get; set; }
        public string? PmsNo { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual EquipmentName? EquipmentName { get; set; }
        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
        public virtual ICollection<JobDescriptionEntryPms> JobDescriptionEntryPms { get; set; }
    }
}
