using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class ItemDetail : BaseDomainEntity
    {
        public ItemDetail()
        {
            Acceptances = new HashSet<Acceptance>();
            Demands = new HashSet<Demand>();
            LifeLimitItemRunningHours = new HashSet<LifeLimitItemRunningHour>();
            Procurements = new HashSet<Procurement>();
            ItemStors = new HashSet<ItemStor>();
            ToolsIssues = new HashSet<ToolsIssue>();
            IssueRegisters = new HashSet<IssueRegister>();
            PreviousItemStores = new HashSet<PreviousItemStore>();
            CallibrationStates = new HashSet<CallibrationState>();
            SurveyItems = new HashSet<SurveyItem>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
            PreventiveMaintenances = new HashSet<PreventiveMaintenance>();
            PmsNumberEntries = new HashSet<PmsNumberEntry>();
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPms>();
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();

        }
     
        public int ItemDetailId { get; set; }
        public int? UnitId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? EquipmentPriorityId { get; set; }
        //public int? EquipmentPriority { get; set; }
        public string? PartNo { get; set; }
        public string? ImcNumber { get; set; }
        public string? SerialNo { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? NameOfItem { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemCategoryTypeId { get; set; }
        public int? SparesCategoryId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? DepartmentNameId { get; set; }
        public string? AlternatiovePrartNo { get; set; }
        public string? MinimumStock { get; set; }
        public int? TradeId { get; set; }
        public string? Spec { get; set; }
        public string? PhotoUp { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        //public virtual BaseSchoolName? Unit { get; set; }
        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual EquipmentName? EquipmentName { get; set; }
        public virtual ItemCategory? ItemCategory { get; set; }
        public virtual EquipmentPriority EquipmentPriority { get; set; }
        public virtual ItemCategoryType? ItemCategoryType { get; set; }
        public virtual SparesCategory? SparesCategory { get; set; }
        public virtual ItemType? ItemType { get; set; }
        public virtual Trade? Trade { get; set; }
        public virtual ICollection<JobDescriptionEntryPms> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<PmsNumberEntry> PmsNumberEntries { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
        public virtual ICollection<PreviousItemStore> PreviousItemStores { get; set; }
        public virtual ICollection<Acceptance> Acceptances { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<PreventiveMaintenance> PreventiveMaintenances { get; set; }
        public virtual ICollection<LifeLimitItemRunningHour> LifeLimitItemRunningHours { get; set; }
        public virtual ICollection<Procurement> Procurements { get; set; }
        public virtual ICollection<ItemStor> ItemStors { get; set; }
        public virtual ICollection<ToolsIssue> ToolsIssues { get; set; }
        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
        public virtual ICollection<CallibrationState> CallibrationStates { get; set; }
        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
    }
}
