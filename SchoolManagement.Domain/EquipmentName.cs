using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class EquipmentName : BaseDomainEntity
    {

        public EquipmentName()
        {
            Demands = new HashSet<Demand>();
            IssueRegisters = new HashSet<IssueRegister>();
            ItemDetails = new HashSet<ItemDetail>();
            ItemStors = new HashSet<ItemStor>();
            SurveyItems= new HashSet<SurveyItem>();
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPms>();
            PmsNumberEntries = new HashSet<PmsNumberEntry>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
        }

        public int EquipmentNameId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? SparesCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual ItemCategory? ItemCategory { get; set; }
        public virtual SparesCategory? SparesCategory { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<PmsNumberEntry> PmsNumberEntries { get; set; }
        public virtual ICollection<JobDescriptionEntryPms> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
        public virtual ICollection<ItemStor> ItemStors { get; set; }
        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
    }
}
