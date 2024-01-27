using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.SurveyItem
{
    public class SurveyItemDto : ISurveyItemDto
    {
        public int SurveyItemId { get; set; }
        public int? DenoId { get; set; }
        public int? ItemStatusId { get; set; }
        public int? ItemDetailId { get; set; }
        public string? Qty { get; set; }
        public string? SurveyNo { get; set; }
        public string? NsdSrNo { get; set; }
        public DateTime? SurveyDate { get; set; }
        public string? ItemSerNo { get; set; }
        public string? ByWhom { get; set; }
        public string? SurveyDocument { get; set; }
        public string? DemandNo { get; set; }
        public string? ReturnStore { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? SurveyAuthorityId { get; set; }
        public int? SurveyItemTypeId { get; set; }
    }
}
