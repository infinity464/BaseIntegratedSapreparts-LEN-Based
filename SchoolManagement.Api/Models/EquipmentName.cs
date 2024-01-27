using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class EquipmentName
    {
        public EquipmentName()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
            Demands = new HashSet<Demand>();
            IssueRegisters = new HashSet<IssueRegister>();
            ItemDetails = new HashSet<ItemDetail>();
            ItemStors = new HashSet<ItemStor>();
            JobDescriptionEntryPms = new HashSet<JobDescriptionEntryPm>();
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
            PmsNumberEntries = new HashSet<PmsNumberEntry>();
            SpareRequiredEntries = new HashSet<SpareRequiredEntry>();
            SurveyItems = new HashSet<SurveyItem>();
        }

        public int EquipmentNameId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? SparesCategoryId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual SparesCategory SparesCategory { get; set; }
        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
        public virtual ICollection<ItemStor> ItemStors { get; set; }
        public virtual ICollection<JobDescriptionEntryPm> JobDescriptionEntryPms { get; set; }
        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
        public virtual ICollection<PmsNumberEntry> PmsNumberEntries { get; set; }
        public virtual ICollection<SpareRequiredEntry> SpareRequiredEntries { get; set; }
        public virtual ICollection<SurveyItem> SurveyItems { get; set; }
    }
}
