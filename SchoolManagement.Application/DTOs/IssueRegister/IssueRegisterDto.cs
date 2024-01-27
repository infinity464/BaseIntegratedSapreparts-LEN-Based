using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.IssueRegister
{
    public class IssueRegisterDto : IIssueRegisterDto
    {
        public int IssueRegisterId { get; set; }
        public int? UnitId { get; set; } 
        public int? DepartmentNameId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? IssueTypeId { get; set; }
        public int? CorrectiveCompleteStatus { get; set; }
        public int? DemandCompleteStatus { get; set; }
        public int? IssueQty { get; set; }
        public int? SurveyStatus { get; set; }
        public string? SurveyNumber { get; set; }
        public DateTime? SurveyDate { get; set; }
        public int? SurveyAuthorityId { get; set; }
        public int? SurveyItemTypeId { get; set; }
        public string? ByWhom { get; set; }
        public int? ToolsLocationId { get; set; } 
        public int? Status { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? IssuedTo { get; set; }
        public string? IssuedBy { get; set; }
        public string? Remarks { get; set; }
        public int? ItemStoreId { get; set; }
        public int? IssueStatusId { get; set; }
        public int? ReturnQty { get; set; } 
        public int? SparesCategoryId { get; set; }
        public int? TotalReceivedQty { get; set; }
        public int? TrainingCrewId { get; set; }
        public int? OplStatus { get; set; }
        public string? Reason { get; set; }
        public bool? IsRefundable { get; set; }
        public int? AvailableQtyBeforeIssue { get; set; }
        public int? AvailableQtyAfterIssue { get; set; }
        public string? ReceivedPerson { get; set; }
        public bool IsActive { get; set; }
         
        public string? Pno { get; set; }
        public string? Name { get; set; }
        public string? ItemCategory { get; set; }
        public string? ItemName { get; set; }
        public string? PartNO { get; set; }
        public string? ToolsLocation { get; set; } 
        public string? SurveyAuthority { get; set; }
        public string? SurveyItemType { get; set; }
        public string? Imc { get; set; }
    }
}
