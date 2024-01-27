using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class BaseSchoolName : BaseDomainEntity
    {
        public BaseSchoolName()
        {
            Acceptances = new HashSet<Acceptance>();
            AirCraftFlyings = new HashSet<AirCraftFlying>();
            AirCraftNames = new HashSet<AirCraftName>();
            CallibrationStates = new HashSet<CallibrationState>();
            Demands = new HashSet<Demand>();
            EquipmentIssues = new HashSet<EquipmentIssue>();
            EquipmentNames = new HashSet<EquipmentName>();
            GseItemNames = new HashSet<GseItemName>();
            GseMaintenanceScheduleNames = new HashSet<GseMaintenanceScheduleName>();
            GseMaintenances = new HashSet<GseMaintenance>();
            GseScheduleWorkTypes = new HashSet<GseScheduleWorkType>();
            IssueRegisters = new HashSet<IssueRegister>();
            ItemDetails = new HashSet<ItemDetail>();
            ItemStors = new HashSet<ItemStor>();
            LifeLimitItemRunningHours = new HashSet<LifeLimitItemRunningHour>();
            MaintenanceCategories = new HashSet<MaintenanceCategory>();
            MaintenancePlannings = new HashSet<MaintenancePlanning>();
            MaintenanceSchedules = new HashSet<MaintenanceSchedule>();
            MaintenanceSubCategories = new HashSet<MaintenanceSubCategory>();
            MaintenanceTypes = new HashSet<MaintenanceType>();
            MeaSquadronStates = new HashSet<MeaSquadronState>();
            PreviousItemStores = new HashSet<PreviousItemStore>();
            Procurements = new HashSet<Procurement>();
            RunningHours = new HashSet<RunningHour>();
            ToolsIssues = new HashSet<ToolsIssue>();
            TrainingCrews = new HashSet<TrainingCrew>();
            SurveyItems = new HashSet<SurveyItem>();
            PmsNumberEntries = new HashSet<PmsNumberEntry>();

            PreventiveMaintenances = new HashSet<PreventiveMaintenance>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPms>();
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
            SurveyItems = new HashSet<SurveyItem>();

        }
        public int BaseSchoolNameId { get; set; }
        public string? SchoolName { get; set; }
        public string? ShortName { get; set; }
        public string? SchoolLogo { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public int? BranchLevel { get; set; }
        public int? FirstLevel { get; set; }
        public int? SecondLevel { get; set; }
        public int? ThirdLevel { get; set; }
        public int? FourthLevel { get; set; }
        public int? FifthLevel { get; set; }
        public string? ServerName { get; set; }

        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
        public virtual ICollection<PmsNumberEntry> PmsNumberEntries { get; set; }
        public virtual ICollection<PreventiveMaintenance> PreventiveMaintenances { get; set; }
        public virtual ICollection<JobDescriptionEntryPms> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<Acceptance> Acceptances { get; set; }
        public virtual ICollection<AirCraftFlying> AirCraftFlyings { get; set; }
        public virtual ICollection<AirCraftName> AirCraftNames { get; set; }
        public virtual ICollection<CallibrationState> CallibrationStates { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<EquipmentIssue> EquipmentIssues { get; set; }
        public virtual ICollection<EquipmentName> EquipmentNames { get; set; }
        public virtual ICollection<GseItemName> GseItemNames { get; set; }
        public virtual ICollection<GseMaintenanceScheduleName> GseMaintenanceScheduleNames { get; set; }
        public virtual ICollection<GseMaintenance> GseMaintenances { get; set; }
        public virtual ICollection<GseScheduleWorkType> GseScheduleWorkTypes { get; set; }
        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
        public virtual ICollection<ItemStor> ItemStors { get; set; }
        public virtual ICollection<LifeLimitItemRunningHour> LifeLimitItemRunningHours { get; set; }
        public virtual ICollection<MaintenanceCategory> MaintenanceCategories { get; set; }
        public virtual ICollection<MaintenancePlanning> MaintenancePlannings { get; set; }
        public virtual ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public virtual ICollection<MaintenanceSubCategory> MaintenanceSubCategories { get; set; }
        public virtual ICollection<MaintenanceType> MaintenanceTypes { get; set; }
        public virtual ICollection<MeaSquadronState> MeaSquadronStates { get; set; }
        public virtual ICollection<PreviousItemStore> PreviousItemStores { get; set; }
        public virtual ICollection<Procurement> Procurements { get; set; }
        public virtual ICollection<RunningHour> RunningHours { get; set; }
        public virtual ICollection<ToolsIssue> ToolsIssues { get; set; }
        public virtual ICollection<TrainingCrew> TrainingCrews { get; set; }
        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
