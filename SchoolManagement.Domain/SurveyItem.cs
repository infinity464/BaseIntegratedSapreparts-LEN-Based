using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class SurveyItem : BaseDomainEntity
    {
        public int SurveyItemId { get; set; }
        public int? DenoId { get; set; }
        public string? ByWhom { get; set; }
        public int? ItemStatusId { get; set; }
        public int? ItemDetailId { get; set; }
        public string? Qty { get; set; }
        public string? SurveyNo { get; set; }
        public string? NsdSrNo { get; set; }
        public DateTime? SurveyDate { get; set; }
        public string? ItemSerNo { get; set; }
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

        public virtual Deno? Deno { get; set; }
        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual EquipmentName? EquipmentName { get; set; }
        public virtual ItemCategory? ItemCategory { get; set; }
        public virtual ItemDetail? ItemDetail { get; set; }
        public virtual ItemStatus? ItemStatus { get; set; }
        public virtual SurveyAuthority? SurveyAuthority { get; set; }
        public virtual SurveyItemType? SurveyItemType { get; set; }
    }
}
