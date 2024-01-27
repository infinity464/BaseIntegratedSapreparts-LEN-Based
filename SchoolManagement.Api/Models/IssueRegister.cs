using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class IssueRegister
    {
        public IssueRegister()
        {
            CorrectiveMaintenences = new HashSet<CorrectiveMaintenence>();
            Demands = new HashSet<Demand>();
            JobDescriptionScheduleMaintenences = new HashSet<JobDescriptionScheduleMaintenence>();
            PreventiveMaintenances = new HashSet<PreventiveMaintenance>();
            ReturnLogs = new HashSet<ReturnLog>();
            ScheduleMaintenances = new HashSet<ScheduleMaintenance>();
            Surveys = new HashSet<Survey>();
        }

        public int IssueRegisterId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? IssueTypeId { get; set; }
        public int? IssueQty { get; set; }
        public int? SurveyStatus { get; set; }
        public string SurveyNumber { get; set; }
        public DateTime? SurveyDate { get; set; }
        public int? SurveyAuthorityId { get; set; }
        public int? SurveyItemTypeId { get; set; }
        public string ByWhom { get; set; }
        public int? TotalReceivedQty { get; set; }
        public int? ReturnQty { get; set; }
        public int? ToolsLocationId { get; set; }
        public int? Status { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssuedTo { get; set; }
        public string IssuedBy { get; set; }
        public string Remarks { get; set; }
        public int? ItemStoreId { get; set; }
        public int? IssueStatusId { get; set; }
        public int? SparesCategoryId { get; set; }
        public int? TrainingCrewId { get; set; }
        public string Reason { get; set; }
        public bool? IsRefundable { get; set; }
        public int? AvailableQtyBeforeIssue { get; set; }
        public int? AvailableQtyAfterIssue { get; set; }
        public string ReceivedPerson { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int? CorrectiveCompleteStatus { get; set; }
        public int? PreventiveCompleteStatus { get; set; }
        public int? ScheduleCompleteStatus { get; set; }
        public int? DemandCompleteStatus { get; set; }

        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual EquipmentName EquipmentName { get; set; }
        public virtual IssueStatus IssueStatus { get; set; }
        public virtual IssueType IssueType { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public virtual ItemDetail ItemDetail { get; set; }
        public virtual ItemStor ItemStore { get; set; }
        public virtual SparesCategory SparesCategory { get; set; }
        public virtual SurveyAuthority SurveyAuthority { get; set; }
        public virtual SurveyItemType SurveyItemType { get; set; }
        public virtual ToolsLocation ToolsLocation { get; set; }
        public virtual TrainingCrew TrainingCrew { get; set; }
        public virtual ICollection<CorrectiveMaintenence> CorrectiveMaintenences { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences { get; set; }
        public virtual ICollection<PreventiveMaintenance> PreventiveMaintenances { get; set; }
        public virtual ICollection<ReturnLog> ReturnLogs { get; set; }
        public virtual ICollection<ScheduleMaintenance> ScheduleMaintenances { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
