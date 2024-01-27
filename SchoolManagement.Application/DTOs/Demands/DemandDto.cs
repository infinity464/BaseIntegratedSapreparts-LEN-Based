namespace SchoolManagement.Application.DTOs.Demands
{
    public class DemandDto : IDemandDto
    {
        public int DemandId { get; set; }
        public int? AuthorityId { get; set; }
        public int? UnitId { get; set; }
        public int? TradeId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? SurveyId { get; set; }
        public int? SupplierId { get; set; }
        public int? ManufactureId { get; set; }
        public int? DenoId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? GroupId { get; set; }
        public int? ItemStatusId { get; set; }
        public int? FiscalYearId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? OccasionOfDemandId { get; set; }
        public int? DemandAuthorityId { get; set; }
        public int? DemandTypeId { get; set; }
        public int? SparesCategoryId { get; set; }
        public int? DemandDocId { get; set; }
        public string? SpecDoc { get; set; }
        public int? ConditionOfItemId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? DemandCompleteStatus { get; set; }
        public string? DemandQty { get; set; }
        public string? DemandLetterNo { get; set; }
        public string? DemandNo { get; set; }
        public DateTime? DemandDate { get; set; }
        public string? LetterOuterNo { get; set; }
        public string? RefPrice { get; set; }
        public string? RefPoNo { get; set; }
        public string? Remarks { get; set; }
        public string? OldPrice { get; set; }
        public string? OldRefNo { get; set; }
        public string? ManufactureAddress { get; set; }
        public string? ImcNo { get; set; }
        public string? SurveyNo { get; set; }
        public string? NameOfDemandee { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
        public int? ProcurementStatus { get; set; }
        public int? StoreStatus { get; set; }
        public int? SftStatus { get; set; }

        public  string? DemandAuthority { get; set; }
        public  string? ItemCategory { get; set; } 
        public  string? Authority { get; set; }
        public string? Supplier { get; set; }
        public  string? Deno { get; set; }
        public  string? FiscalYear { get; set; }
        public string? Manufacture { get; set; }
        public string? OccasionOfDemand { get; set; }
        public string? ConditionOfItem { get; set; }
        public string? DepartmentName { get; set; }
        public string? ItemDetail { get; set; }
        public string? PartNo { get; set; }
        public string? ItemName { get; set; }  
        public string? ImcNumber { get; set; }
        public string? ItemStatus { get; set; }
        public string? EquipmentName { get; set; }
      //  public int? SurveyId { get; set; } 

    }
}
